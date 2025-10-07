using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Common.Services;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductResult>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<GetProductQueryHandler> _logger;
    private readonly IRedisCacheService _cache;

    public GetProductQueryHandler(IProductRepository repository, ILogger<GetProductQueryHandler> logger, IRedisCacheService cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando produto com ID {Id}", request.Id);

        var cacheKey = $"products:{request.Id}";
        var product = await _cache.GetAsync<Product>(cacheKey);

        if (product == null)
        {
            product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product != null)
                await _cache.SetAsync(cacheKey, product, TimeSpan.FromMinutes(10));
        }

        return new GetProductResult { Product = product };
    }
}
