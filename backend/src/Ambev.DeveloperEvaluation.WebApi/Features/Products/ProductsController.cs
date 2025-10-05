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

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.Price,
                request.StockQuantity
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
        public async Task<IActionResult> GetAllProducts([FromServices] IProductRepository repository, CancellationToken cancellationToken)
        {
            var products = await repository.GetAllAsync(cancellationToken);
            return Ok(new
            {
                success = true,
                data = products
            });
        }
    }
}
