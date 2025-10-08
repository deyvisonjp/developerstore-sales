namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers.Create;
public class CreateSaleResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
