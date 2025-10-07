using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Common.Services;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<DeleteProductCommandHandler> _logger;
    private readonly IRedisCacheService _cache;

    public DeleteProductCommandHandler(
        IProductRepository repository,
        ILogger<DeleteProductCommandHandler> logger,
        IRedisCacheService cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removendo produto com ID {Id}", request.Id);

        var success = await _repository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado.");

        await _cache.RemoveAsync($"products:{request.Id}");
        await _cache.RemoveAsync("products:all");

        return true;
    }
}
