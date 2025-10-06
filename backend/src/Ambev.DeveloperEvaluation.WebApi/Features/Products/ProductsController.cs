using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(
                Id: Guid.NewGuid(),
                Name: request.Name,
                Description: request.Description,
                Category: request.Category,
                Price: request.Price,
                RatingAverage: request.RatingAverage,
                RatingReviews: request.RatingReviews
            );

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                success = true,
                message = "Product created successfully",
                data = result
            });
        }

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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return NotFound(new { success = false, message = "Product not found" });

            return Ok(new { success = true, data = product });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _repository.GetCategoriesAsync(cancellationToken);
            return Ok(new { success = true, data = categories });
        }

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
    }
}
