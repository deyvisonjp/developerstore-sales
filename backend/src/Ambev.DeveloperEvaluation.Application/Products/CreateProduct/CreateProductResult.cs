using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Resultado da criação de um produto.
    /// </summary>
    public class CreateProductResult
    {
        public Product Product { get; set; } = default!;
    }
}
