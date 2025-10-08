using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleCreateDto, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) 
                .ForMember(dest => dest.Status, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItemDto, SaleItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SaleId, opt => opt.Ignore());

            CreateMap<Sale, SaleReadDto>();
            CreateMap<SaleItem, SaleItemDto>();
        }
    }
}
