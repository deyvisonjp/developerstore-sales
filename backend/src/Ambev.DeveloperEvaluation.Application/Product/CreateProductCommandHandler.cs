using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler responsável por criar um novo produto.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IProductRepository productRepository, 
        IMapper mapper,
        ILogger<CreateProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var product = _mapper.Map<Product>(command);
            await _productRepository.AddAsync(product, cancellationToken);
            _logger.LogInformation("Product {ProductName} created successfully with Id {ProductId}", product.Name, product.Id);
            return _mapper.Map<CreateProductResult>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create product {ProductName}", command.Name);
            throw;
        }

    }
}
