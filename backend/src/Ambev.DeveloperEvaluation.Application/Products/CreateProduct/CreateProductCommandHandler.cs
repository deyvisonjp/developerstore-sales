using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Handler para criação de produtos.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            IProductRepository repository,
            IMapper mapper,
            ILogger<CreateProductCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando criação de produto: {Name}", request.Name);

            var product = _mapper.Map<Product>(request);

            var createdProduct = await _repository.CreateAsync(product, cancellationToken);

            _logger.LogInformation("Produto criado com sucesso: {ProductId}", createdProduct.Id);

            var result = _mapper.Map<CreateProductResult>(createdProduct);

            return result;
        }
    }
}
