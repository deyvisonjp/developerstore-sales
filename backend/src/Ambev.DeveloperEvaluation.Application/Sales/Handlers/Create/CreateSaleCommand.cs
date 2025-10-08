using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers.Create
{
    public class CreateSaleCommand : IRequest<SaleResponseDto>
    {
        public SaleCreateDto Dto { get; }
        public CreateSaleCommand(SaleCreateDto dto)
        {
            Dto = dto;
        }
    }
}