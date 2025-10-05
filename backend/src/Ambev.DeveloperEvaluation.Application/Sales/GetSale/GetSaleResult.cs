namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Result returned when retrieving a sale
    /// </summary>
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerDocument { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalWithDiscount { get; set; }
        public ICollection<GetSaleItemResult> Items { get; set; } = new List<GetSaleItemResult>();
    }
}
