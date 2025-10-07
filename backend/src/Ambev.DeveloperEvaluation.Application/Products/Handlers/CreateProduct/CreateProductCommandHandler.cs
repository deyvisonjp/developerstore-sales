
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Common.Services;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductReadDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _cache;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper, IRedisCacheService cache)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<ProductReadDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.Dto);
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;

        var created = await _repository.CreateAsync(product, cancellationToken);
        await _cache.SetAsync($"products:{created.Id}", created);
        await _cache.RemoveAsync("products:all");

        return _mapper.Map<ProductReadDto>(created);
    }
}
