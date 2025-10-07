using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}