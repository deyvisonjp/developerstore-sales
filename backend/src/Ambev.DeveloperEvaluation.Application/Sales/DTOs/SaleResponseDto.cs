using Ambev.DeveloperEvaluation.Application.SaleItems.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public class SaleResponseDto
    {
        public string SaleNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<SaleItemResponseDto> Items { get; set; } = new();
    }
}
