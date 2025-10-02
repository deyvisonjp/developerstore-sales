using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
        Task<Sale> RegisterSaleAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> CancelSaleAsync(Guid saleId, CancellationToken cancellationToken = default);
}
