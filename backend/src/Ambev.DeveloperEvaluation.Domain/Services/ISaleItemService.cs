using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleItemService
{
    Task<SaleItem> AddItemAsync(Guid saleId, SaleItem saleItem, CancellationToken cancellationToken = default);
}
