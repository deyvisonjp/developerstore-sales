using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Create;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Configurações de mapeamento para Product requests e commands.
    /// </summary>
    public class ProductRequestProfile : Profile
    {
        public ProductRequestProfile()
        {

            CreateMap<CreateProductRequest, CreateProductCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
