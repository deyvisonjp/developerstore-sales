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
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SaleReadDto>>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SaleReadDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<SaleReadDto>>(sales);
        }
    }
}