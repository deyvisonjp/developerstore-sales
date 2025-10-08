using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers.Delete
{
    public record DeleteSaleCommand(Guid Id) : IRequest<bool>;
}