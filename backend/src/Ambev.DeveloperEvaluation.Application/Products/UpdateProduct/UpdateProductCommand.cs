using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using MediatR;

public record UpdateProductCommand(
    Guid Id,
    string Title,
    decimal Price,
    string Description,
    string Category,
    string? Image,
    double Rate,
    decimal Reviews,
    int Count
) : IRequest<UpdateProductResult>;
