using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.CreateProduct
{
    /// <summary>
    /// Comando para criação de um novo produto.
    /// </summary>
    public record CreateProductCommand(ProductCreateDto Dto) : IRequest<ProductReadDto>;
}