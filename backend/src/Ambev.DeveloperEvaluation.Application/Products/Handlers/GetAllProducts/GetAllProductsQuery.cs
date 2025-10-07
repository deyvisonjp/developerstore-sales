using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.GetAllProducts;

/// <summary>
/// Query para obter a lista de produtos paginada.
/// </summary>
public record GetAllProductsQuery(
    int Page = 1,
    int Size = 10,
    string? Order = null
) : IRequest<GetAllProductsResult>;
