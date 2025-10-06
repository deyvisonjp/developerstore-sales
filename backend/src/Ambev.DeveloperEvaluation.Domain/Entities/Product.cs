using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa um produto disponível no catálogo da aplicação.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Descrição detalhada do produto.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Categoria do produto
    /// </summary>
    public string Category { get; set; } = default!;

    /// <summary>
    /// URL da imagem representativa do produto.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Avaliação média do produto (0 a 5).
    /// </summary>
    public double RatingAverage { get; set; }

    /// <summary>
    /// Quantidade de avaliações do produto.
    /// </summary>
    public int RatingReviews { get; set; }

    /// <summary>
    /// Data e hora da criação do produto.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Data e hora da última atualização do produto.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Relacionamento com os itens de venda.
    /// </summary>
    public ICollection<SaleItem>? SaleItems { get; set; }
}
