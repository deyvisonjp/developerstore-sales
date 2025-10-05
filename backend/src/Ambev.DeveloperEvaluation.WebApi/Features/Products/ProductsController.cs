//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using AutoMapper;
//using Ambev.DeveloperEvaluation.WebApi.Common;
//using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;


//namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

///// <summary>
///// Controller for managing product operations.
///// </summary>
//[ApiController]
//[Route("api/[controller]")]
//public class ProductsController : BaseController
//{
//    private readonly IMediator _mediator;
//    private readonly IMapper _mapper;

//    public ProductsController(IMediator mediator, IMapper mapper)
//    {
//        _mediator = mediator;
//        _mapper = mapper;
//    }

//    [HttpPost]
//    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
//    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
//    {
//        var validator = new CreateProductRequestValidator();
//        var validationResult = await validator.ValidateAsync(request, cancellationToken);

//        if (!validationResult.IsValid)
//            return BadRequest(validationResult.Errors);

//        var command = _mapper.Map<CreateProductCommand>(request);
//        var result = await _mediator.Send(command, cancellationToken);

//        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
//        {
//            Success = true,
//            Message = "Product created successfully",
//            Data = _mapper.Map<CreateProductResponse>(result)
//        });
//    }

//    [HttpGet("{id}")]
//    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
//    {
//        var request = new GetProductRequest { Id = id };
//        var validator = new GetProductRequestValidator();
//        var validationResult = await validator.ValidateAsync(request, cancellationToken);

//        if (!validationResult.IsValid)
//            return BadRequest(validationResult.Errors);

//        var command = new GetProductCommand(id);
//        var result = await _mediator.Send(command, cancellationToken);

//        return Ok(new ApiResponseWithData<GetProductResponse>
//        {
//            Success = true,
//            Message = "Product retrieved successfully",
//            Data = _mapper.Map<GetProductResponse>(result)
//        });
//    }

//    [HttpDelete("{id}")]
//    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
//    {
//        var request = new DeleteProductRequest { Id = id };
//        var validator = new DeleteProductRequestValidator();
//        var validationResult = await validator.ValidateAsync(request, cancellationToken);

//        if (!validationResult.IsValid)
//            return BadRequest(validationResult.Errors);

//        var command = new DeleteProductCommand(id);
//        await _mediator.Send(command, cancellationToken);

//        return Ok(new ApiResponse
//        {
//            Success = true,
//            Message = "Product deleted successfully"
//        });
//    }
//}
