using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.Handlers.CreateProduct;
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
            CreateMap<CreateProductRequest, ProductCreateDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new RatingDto
                {
                    Rate = src.RatingAverage,
                    Count = (int)src.RatingReviews
                }));

            CreateMap<ProductCreateDto, CreateProductCommand>()
                .ConstructUsing(dto => new CreateProductCommand(dto));
        }
    }
}
