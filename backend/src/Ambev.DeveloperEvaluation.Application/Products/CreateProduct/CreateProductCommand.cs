using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Comando para criação de um novo produto.
    /// </summary>
    public record CreateProductCommand(
        Guid Id,
        string Title,
        string? Description,
        string? Category,
        string? Image,
        decimal Price,
        double RatingAverage,
        decimal RatingReviews
    ) : IRequest<CreateProductResult>;
}
