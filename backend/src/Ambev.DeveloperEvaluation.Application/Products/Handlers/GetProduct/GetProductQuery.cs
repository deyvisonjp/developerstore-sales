using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<GetProductResult>;
