using Microsoft.OpenApi.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Developer Evaluation API",
                Version = "v1",
                Description = "API backend desenvolvida em .NET 8 para desafio técnico."
            });
        });

        return services;
    }
}
