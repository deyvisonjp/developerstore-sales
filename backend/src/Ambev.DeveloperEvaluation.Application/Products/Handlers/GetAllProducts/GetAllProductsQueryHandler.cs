using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Common.Services;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResult>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;
    private readonly IRedisCacheService _cache;

    public GetAllProductsQueryHandler(IProductRepository repository, ILogger<GetAllProductsQueryHandler> logger, IRedisCacheService cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando produtos (Page: {Page}, Size: {Size}, Order: {Order})", request.Page, request.Size, request.Order);

        var cacheKey = "products:all";
        var products = await _cache.GetAsync<IEnumerable<Domain.Entities.Product>>(cacheKey);

        if (products == null)
        {
            products = await _repository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);
            await _cache.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));
        }

        var totalItems = await _repository.GetTotalCountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalItems / request.Size);

        return new GetAllProductsResult
        {
            Data = products,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = totalPages
        };
    }
}
