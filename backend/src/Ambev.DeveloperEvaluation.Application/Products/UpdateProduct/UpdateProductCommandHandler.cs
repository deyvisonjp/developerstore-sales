using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductResult?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Atualizando produto {ProductId}", request.Id);

        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            _logger.LogWarning("Produto {ProductId} não encontrado", request.Id);
            return null;
        }

        existing.Name = request.Title;
        existing.Price = request.Price;
        existing.Category = request.Category;

        await _repository.UpdateAsync(existing, cancellationToken);

        return _mapper.Map<UpdateProductResult>(existing);
    }
}
