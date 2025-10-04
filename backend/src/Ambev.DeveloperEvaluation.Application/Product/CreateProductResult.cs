namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Resultado da criação de produto.
/// </summary>
public class CreateProductResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
