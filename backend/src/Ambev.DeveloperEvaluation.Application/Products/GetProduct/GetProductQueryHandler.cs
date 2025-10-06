using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductResult?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductQueryHandler> _logger;

    public GetProductQueryHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<GetProductQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetProductResult?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando produto {ProductId}", request.Id);

        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            _logger.LogWarning("Produto {ProductId} não encontrado", request.Id);
            return null;
        }

        return _mapper.Map<GetProductResult>(product);
    }
}
