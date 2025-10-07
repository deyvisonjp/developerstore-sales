using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Resultado da atualização de um produto.
/// </summary>
public class UpdateProductResult
{
    public Product Product { get; set; } = default!;
}
