using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }
    public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Sale sale = await _context.Sales
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        var affected = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return affected > 0;
    }
}
