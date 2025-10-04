using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Handler for processing CreateSaleItemCommand requests
    /// </summary>
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateSaleItemHandler(
            ISaleItemRepository saleItemRepository,
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _saleItemRepository = saleItemRepository;
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken)
                ?? throw new InvalidOperationException($"Sale {command.SaleId} not found");

            var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
                ?? throw new InvalidOperationException($"Product {command.ProductId} not found");

            var saleItem = new SaleItem
            {
                Id = Guid.NewGuid(),
                SaleId = sale.Id,
                ProductId = product.Id,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice
            };

            var createdItem = await _saleItemRepository.CreateAsync(saleItem, cancellationToken);
            var result = _mapper.Map<CreateSaleItemResult>(createdItem);
            return result;
        }
    }
}
