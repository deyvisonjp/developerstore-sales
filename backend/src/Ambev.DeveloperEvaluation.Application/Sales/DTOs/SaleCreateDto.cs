namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs;

public class SaleCreateDto
{
    public string SaleNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<SaleItemDto> Items { get; set; } = new();
}