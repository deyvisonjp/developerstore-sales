using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa um produto disponível para venda.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Descrição opcional do produto.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Quantidade disponível em estoque.
    /// </summary>
    public int StockQuantity { get; set; }
    public ICollection<SaleItem>? SaleItems { get; set; }
}
