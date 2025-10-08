using Microsoft.OpenApi.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions
{
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

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Insira o token JWT desta forma: **Bearer {seu_token}**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme,
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
