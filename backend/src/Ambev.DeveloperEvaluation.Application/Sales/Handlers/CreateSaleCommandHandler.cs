using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleItems.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleResponseDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<SaleResponseDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("A venda deve conter ao menos um item.");

            var products = new List<Product>();
            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new ArgumentException($"Produto {item.ProductId} não existe.");
                products.Add(product);
            }

            var sale = new Sale
            {
                SaleNumber = dto.SaleNumber,
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                BranchId = dto.BranchId,
                BranchName = dto.BranchName,
            };

            foreach (var itemDto in dto.Items)
            {
                var product = products.First(p => p.Id == itemDto.ProductId);

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

            return response;
        }
    }
}
