using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers.Get
{
    public record GetAllSalesQuery() : IRequest<List<SaleReadDto>>;
}
