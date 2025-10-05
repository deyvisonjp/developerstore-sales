using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for deleting a sale by its ID
    /// </summary>
    public record DeleteSaleCommand(Guid Id) : IRequest<Unit>;
}
