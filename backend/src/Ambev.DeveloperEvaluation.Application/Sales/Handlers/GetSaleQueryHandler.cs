using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, SaleReadDto?>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetSaleQueryHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleReadDto?> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return sale is null ? null : _mapper.Map<SaleReadDto>(sale);
        }
    }
}