using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Handler responsável por processar a listagem paginada de produtos.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<GetAllProductsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando produtos - Página: {Page}, Tamanho: {Size}", request.Page, request.Size);

        var products = await _repository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);
        var totalItems = await _repository.GetTotalCountAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

        var result = new GetAllProductsResult
        {
            Data = _mapper.Map<IEnumerable<ProductItem>>(products),
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = totalPages
        };

        _logger.LogInformation("Retornando {Count} produtos", result.Data.Count());

        return result;
    }
}
