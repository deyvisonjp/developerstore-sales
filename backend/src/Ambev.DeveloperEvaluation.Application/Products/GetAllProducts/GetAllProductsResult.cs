namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Modelo de resposta paginada de produtos.
/// </summary>
public class GetAllProductsResult
{
    /// <summary>
    /// Lista de produtos retornados.
    /// </summary>
    public IEnumerable<ProductItem> Data { get; set; } = new List<ProductItem>();

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

/// <summary>
/// Item de produto retornado na listagem.
/// </summary>
public class ProductItem
{
    /// <summary>
    /// Identificador único do produto.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Categoria do produto.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Preço do produto.
    /// </summary>
    public decimal Price { get; set; }
}
