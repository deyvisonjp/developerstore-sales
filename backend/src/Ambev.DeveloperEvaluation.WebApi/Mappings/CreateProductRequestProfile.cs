using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
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

            CreateMap<Product, CreateProductResult>()
                .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.RatingAverage))
                .ForMember(dest => dest.RatingReviews, opt => opt.MapFrom(src => src.RatingReviews));
        }
    }
}
