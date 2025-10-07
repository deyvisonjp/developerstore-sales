using Ambev.DeveloperEvaluation.Application;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationLayer).Assembly);
            return services;
        }
    }
}
