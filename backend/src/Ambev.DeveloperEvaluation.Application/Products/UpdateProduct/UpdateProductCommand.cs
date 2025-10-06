using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image,
    decimal Rate,
    int Count
) : IRequest<UpdateProductResult>;
