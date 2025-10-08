using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Update
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.RatingReviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
