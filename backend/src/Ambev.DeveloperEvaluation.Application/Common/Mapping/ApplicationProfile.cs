using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

namespace Ambev.DeveloperEvaluation.Application.Common.Mapping
{
    /// <summary>
    /// Central AutoMapper profile used to map between application DTOs and domain entities.
    /// </summary>
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            AddSaleMappings();
        }

        private void AddSaleMappings()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItemDto, SaleItemDto>();

            CreateMap<Sale, CreateSaleResult>();
            CreateMap<SaleItemDto, CreateSaleItemResult>();
        }
    }
}
