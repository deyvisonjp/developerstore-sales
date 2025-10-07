using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Common.Services;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IRedisCacheService _cache;

    public UpdateProductCommandHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<UpdateProductCommandHandler> logger,
        IRedisCacheService cache)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _cache = cache;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Atualizando produto {Id}", request.Id);

        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
            throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado.");

        _mapper.Map(request, existing);
        var updated = await _repository.UpdateAsync(existing, cancellationToken);

        await _cache.SetAsync($"products:{updated.Id}", updated);
        await _cache.RemoveAsync("products:all");

        return new UpdateProductResult { Product = updated };
    }
}
