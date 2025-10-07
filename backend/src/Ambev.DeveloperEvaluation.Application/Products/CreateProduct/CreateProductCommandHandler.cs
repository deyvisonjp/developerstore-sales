using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Title,
            Description = request.Description,
            Category = request.Category,
            Price = request.Price,
            ImageUrl = request.Image,
            RatingAverage = request.RatingAverage,
            RatingReviews = (int)request.RatingReviews,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(product, cancellationToken);

        return new CreateProductResult { Product = product };
    }
}
