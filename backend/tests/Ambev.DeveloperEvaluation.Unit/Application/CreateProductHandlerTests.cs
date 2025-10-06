using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Testes unitários para <see cref="CreateProductCommandHandler"/>.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _logger = Substitute.For<ILogger<CreateProductCommandHandler>>();
        _handler = new CreateProductCommandHandler(_productRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new CreateProductCommand(
            Id: Guid.NewGuid(),
            Name: "Cerveja IPA",
            Description: "Cerveja artesanal de alta fermentação.",
            Category: "Bebidas",
            Price: 12.90m,
            RatingAverage: 4.5,
            RatingReviews: 120
        );

        var createdProduct = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            Price = command.Price,
            RatingAverage = command.RatingAverage,
            RatingReviews = (int)command.RatingReviews,
            CreatedAt = DateTime.UtcNow
        };

        _productRepository
            .CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(createdProduct);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(createdProduct.Id);
        result.Name.Should().Be(createdProduct.Name);
        result.Description.Should().Be(createdProduct.Description);
        result.Category.Should().Be(createdProduct.Category);
        result.Price.Should().Be(createdProduct.Price);
        result.RatingAverage.Should().Be(createdProduct.RatingAverage);
        result.RatingReviews.Should().Be(createdProduct.RatingReviews);

        await _productRepository
            .Received(1)
            .CreateAsync(Arg.Is<Product>(p =>
                p.Id == command.Id &&
                p.Name == command.Name &&
                p.Category == command.Category &&
                p.Price == command.Price &&
                p.RatingAverage == command.RatingAverage &&
                p.RatingReviews == (int)command.RatingReviews
            ), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product data When creating product Then validation fails")]
    public async Task Handle_InvalidRequest_ValidationFails()
    {
        // Given
        var command = new CreateProductCommand(
            Id: Guid.NewGuid(),
            Name: "",
            Description: null,
            Category: "",
            Price: 0,
            RatingAverage: -1,
            RatingReviews: -5
        );

        var validator = new CreateProductCommandValidator();

        // When
        var validationResult = await validator.ValidateAsync(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.Name));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.Category));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.Price));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.RatingAverage));
        validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(command.RatingReviews));
    }
}
