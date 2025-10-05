namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Request para criação de um produto via API.
    /// </summary>
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
