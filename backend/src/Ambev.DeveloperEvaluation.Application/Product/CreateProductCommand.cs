using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Comando para criação de um novo produto.
/// </summary>
public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    int StockQuantity
) : IRequest<CreateProductResult>;
