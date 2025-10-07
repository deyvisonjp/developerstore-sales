using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(connection, b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")));

        return services;
    }
}
