using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken)
    {
        await _context.SaleItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken)
    {
        var itemSale = await _context.SaleItems
            .Where(i =>  i.SaleId == saleId)
            .ToListAsync(cancellationToken);

        return itemSale;
    }
}
