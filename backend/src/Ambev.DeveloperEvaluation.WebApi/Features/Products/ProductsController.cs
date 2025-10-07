using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Create;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Controller responsável por gerenciar produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _repository;

        public ProductsController(IMediator mediator, IProductRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="request">Dados do produto a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Produto criado.</returns>
        /// <response code="200">Produto criado com sucesso.</response>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var dto = new ProductCreateDto
            {
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Price = request.Price,
                Image = request.Image,
                Rating = new RatingDto
                {
                    Rate = request.RatingAverage,
                    Count = (int)request.RatingReviews,
                }
            };

            var command = new CreateProductCommand(dto);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                success = true,
                message = "Product created successfully",
                data = result
            });
        }



        /// <summary>
        /// Recupera todos os produtos com paginação opcional.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <param name="page">Página atual (default 1).</param>
        /// <param name="size">Quantidade de itens por página (default 10).</param>
        /// <param name="order">Ordem de classificação (ex: "price desc, title asc").</param>
        /// <returns>Lista paginada de produtos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken, int page = 1, int size = 10, string? order = null)
        {
            var products = await _repository.GetAllAsync(page, size, order);
            var totalItems = await _repository.GetTotalCountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalItems / (double)size);

            return Ok(new
            {
                success = true,
                data = products,
                totalItems,
                currentPage = page,
                totalPages
            });
        }

        /// <summary>
        /// Recupera um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Produto encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return NotFound(new { success = false, message = "Product not found" });

            return Ok(new { success = true, data = product });
        }

        /// <summary>
        /// Recupera todas as categorias de produtos.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de categorias.</returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _repository.GetCategoriesAsync(cancellationToken);
            return Ok(new { success = true, data = categories });
        }

        /// <summary>
        /// Recupera produtos de uma categoria específica.
        /// </summary>
        /// <param name="category">Nome da categoria.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <param name="page">Página atual (default 1).</param>
        /// <param name="size">Quantidade de itens por página (default 10).</param>
        /// <param name="order">Ordem de classificação.</param>
        /// <returns>Lista de produtos da categoria.</returns>
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category, CancellationToken cancellationToken, int page = 1, int size = 10, string? order = null)
        {
            var products = await _repository.GetByCategoryAsync(category, page, size, order);
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)size);

            return Ok(new
            {
                success = true,
                data = products,
                totalItems,
                currentPage = page,
                totalPages
            });
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="request">Dados atualizados do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Produto atualizado.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(
                Id: id,
                Title: request.Title,
                Description: request.Description,
                Category: request.Category,
                Price: request.Price,
                Image: request.Image,
                Rate: request.RatingAverage,
                Reviews: request.RatingReviews,
                Count: request.RatingReviews
            );

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                success = true,
                message = "Product updated successfully",
                data = result.Product
            });
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);

            await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                success = true,
                message = "Product deleted successfully"
            });
        }
    }
}
