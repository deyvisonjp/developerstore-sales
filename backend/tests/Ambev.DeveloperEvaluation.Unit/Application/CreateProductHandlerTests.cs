using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Unit tests for the <see cref="CreateProductCommandHandler"/>.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductCommandHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            StockQuantity = command.StockQuantity
        };

        var result = new CreateProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);
        _productRepository.AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // When
        var response = await _handler.Handle(command, CancellationToken.None);

        // Then
        response.Should().NotBeNull();
        response.Id.Should().Be(product.Id);
        response.Name.Should().Be(product.Name);
        response.Price.Should().Be(product.Price);
        response.StockQuantity.Should().Be(product.StockQuantity);

        await _productRepository.Received(1).AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c => c == command));
        _mapper.Received(1).Map<CreateProductResult>(Arg.Is<Product>(p => p == product));
    }

    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand(
            Name: "",
            Description: null,
            Price: 0,
            StockQuantity: -1 
        );

        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.Name));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.Price));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.StockQuantity));
    }
}
