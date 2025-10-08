using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.SaleItems.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers.Create
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleResponseDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<CreateSaleCommandHandler> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CreateSaleCommandHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IConnectionMultiplexer redis,
            ILogger<CreateSaleCommandHandler> logger)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _redis = redis;
            _logger = logger;
        }

        public async Task<SaleResponseDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto ?? throw new ArgumentNullException(nameof(request.Dto));

            if (string.IsNullOrWhiteSpace(dto.SaleNumber))
                throw new ArgumentException("SaleNumber é obrigatório.");

            if (dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("A venda deve conter ao menos um item.");

            if (dto.Items.Any(i => i.Quantity <= 0))
                throw new ArgumentException("A quantidade de cada item deve ser maior que zero.");

            var cacheKey = $"sale:{dto.SaleNumber}";
            var db = _redis.GetDatabase();

            try
            {
                var cachedValue = await db.StringGetAsync(cacheKey);
                if (!cachedValue.IsNullOrEmpty)
                {
                    _logger.LogInformation("Venda {SaleNumber} retornada do cache", dto.SaleNumber);
                    var cachedResponse = JsonSerializer.Deserialize<SaleResponseDto>(cachedValue, _jsonOptions);
                    if (cachedResponse != null)
                        return cachedResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha ao acessar cache Redis (não fatal). Continuando processamento...");
            }

            _logger.LogInformation("Criando venda {SaleNumber} para cliente {Customer}", dto.SaleNumber, dto.CustomerName);

            var products = new List<Product>();
            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new ArgumentException($"Produto {item.ProductId} não encontrado.");
                products.Add(product);
            }

            var sale = new Sale
            {
                SaleNumber = dto.SaleNumber,
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                BranchId = dto.BranchId,
                BranchName = dto.BranchName
            };

            foreach (var itemDto in dto.Items)
            {
                var product = products.First(p => p.Id == itemDto.ProductId);

                if (itemDto.Quantity > 20)
                    throw new ArgumentException($"Não é permitido vender mais de 20 unidades do produto {product.Name}.");

                var saleItem = new SaleItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                };

                if (saleItem.Quantity >= 10)
                    saleItem.DiscountPercent = 20;
                else if (saleItem.Quantity >= 4)
                    saleItem.DiscountPercent = 10;
                else
                    saleItem.DiscountPercent = 0;

                saleItem.CalculateAmounts();
                sale.Items.Add(saleItem);
            }

            sale.CalculateTotal();

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var response = new SaleResponseDto
            {
                SaleNumber = createdSale.SaleNumber,
                TotalAmount = createdSale.TotalAmount,
                Status = createdSale.Status.ToString(),
                Items = createdSale.Items.Select(i =>
                {
                    var product = products.First(p => p.Id == i.ProductId);
                    return new SaleItemResponseDto
                    {
                        ProductName = product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        DiscountPercent = i.DiscountPercent,
                        DiscountAmount = i.DiscountAmount,
                        TotalAmount = i.TotalAmount
                    };
                }).ToList()
            };

            try
            {
                await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(response, _jsonOptions), TimeSpan.FromHours(1));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha ao gravar cache para venda {SaleNumber}", dto.SaleNumber);
            }

            _logger.LogInformation("Venda {SaleNumber} criada com sucesso e armazenada em cache", dto.SaleNumber);

            return response;
        }
    }
}
