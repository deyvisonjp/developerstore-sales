using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Request para criação de um produto via API.
    /// </summary>
    public class CreateProductRequest
    {
        [DefaultValue("Notebook")]
        public string Name { get; set; } = "Notebook";
        [DefaultValue("Notebook Dell")]
        public string? Description { get; set; } = "Notebook Dell";
        [DefaultValue(1800)]
        public decimal Price { get; set; } = 1788;
        [DefaultValue(29)]
        public int StockQuantity { get; set; } = 20;
    }
}
