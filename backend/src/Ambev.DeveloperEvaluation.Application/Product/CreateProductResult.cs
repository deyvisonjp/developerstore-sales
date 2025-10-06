namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Resultado da criação de produto.
/// </summary>
public record CreateProductResult(
    Guid Id,
    string Name,
    string? Description,
    string? Category,
    decimal Price,
    double RatingAverage,
    decimal RatingReviews
);
