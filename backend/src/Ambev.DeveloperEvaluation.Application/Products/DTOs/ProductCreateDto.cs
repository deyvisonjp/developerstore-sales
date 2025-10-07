namespace Ambev.DeveloperEvaluation.Application.Products.DTOs;
public class ProductCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public RatingDto Rating { get; set; } = new();
}
