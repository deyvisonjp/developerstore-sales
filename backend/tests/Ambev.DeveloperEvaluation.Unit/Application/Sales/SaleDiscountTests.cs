using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class SaleDiscountTests
{
    private class Sale
    {
        public List<SaleItem> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }

    private class SaleItem
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int DiscountPercent { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice * (1 - DiscountPercent / 100m);
    }

    private class SaleHandler
    {
        public Task<Sale> CreateSaleAsync(List<SaleItem> items)
        {
            foreach (var item in items)
            {
                if (item.Quantity >= 10) item.DiscountPercent = 20;
                else if (item.Quantity >= 5) item.DiscountPercent = 10;
                else item.DiscountPercent = 0;
            }
            var sale = new Sale { Items = items };
            return Task.FromResult(sale);
        }
    }

    [Fact]
    public async Task CreateSale_ShouldApplyDiscountsCorrectly()
    {
        var items = new List<SaleItem>
        {
            new SaleItem { Quantity = 5, UnitPrice = 100 },
            new SaleItem { Quantity = 12, UnitPrice = 50 }
        };

        var handler = new SaleHandler();
        var sale = await handler.CreateSaleAsync(items);

        Assert.Equal(2, sale.Items.Count);
        Assert.Equal(10, sale.Items[0].DiscountPercent);
        Assert.Equal(20, sale.Items[1].DiscountPercent);

        var expectedTotal = (5 * 100 * 0.9m) + (12 * 50 * 0.8m);
        Assert.Equal(expectedTotal, sale.TotalAmount);
    }
}
