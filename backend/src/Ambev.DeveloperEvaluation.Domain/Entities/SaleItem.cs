using Ambev.DeveloperEvaluation.Domain.Services;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public string ProductId { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalItem { get; set; }

        public Sale Sale { get; set; } = default!;

        public SaleItem(string productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Recalculate();
        }

        public void Recalculate()
        {
            Discount = DiscountService.GetDiscountForQuantity(Quantity);
            TotalItem = Quantity * UnitPrice * (1 - Discount);
        }
    }
}