using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.Rating.Rate))
            .ForMember(dest => dest.RatingReviews, opt => opt.MapFrom(src => src.Rating.Count));

        CreateMap<Product, ProductReadDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new RatingDto
            {
                Rate = src.RatingAverage,
                Count = src.RatingReviews
            }));
    }
}
