using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Modelo de resposta paginada de produtos.
/// </summary>
public class GetAllProductsResult
{
    /// <summary>
    /// Lista de produtos retornados.
    /// </summary>
    public IEnumerable<Product> Data { get; set; } = new List<Product>();

    /// <summary>
    /// Total de produtos disponíveis.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Página atual.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total de páginas.
    /// </summary>
    public int TotalPages { get; set; }
}
