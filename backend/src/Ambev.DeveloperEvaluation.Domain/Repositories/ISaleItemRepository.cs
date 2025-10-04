using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface ISaleItemRepository
{
    Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken);
    Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken);
}
