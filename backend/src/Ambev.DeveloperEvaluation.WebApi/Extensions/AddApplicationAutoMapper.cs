using Ambev.DeveloperEvaluation.Application;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class MediatRAutoMapperExtensions
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ApplicationLayer).Assembly);
        });
        return services;
    }

    public static IServiceCollection AddApplicationAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationLayer).Assembly);
        return services;
    }
}
