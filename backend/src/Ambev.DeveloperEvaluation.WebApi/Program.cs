using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.WebApi.Extensions;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Ambev.DeveloperEvaluation.Common.Security;
using DotNetEnv;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Serilog
        Log.Logger = SerilogExtensions.ConfigureBootstrapLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                             .ReadFrom.Services(services)
                             .Enrich.FromLogContext());

            // Load .env
            Env.Load();
            builder.Configuration.ApplyEnvVariables();

            // IoC
            builder.RegisterDependencies();

            // Conf WebAPI
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocs();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.AddBasicHealthChecks();

            // Infra
            builder.Services.AddDbContextConfiguration(builder.Configuration);
            builder.Services.AddRedisConfiguration(builder.Configuration);
            builder.Services.AddApplicationAutoMapper();
            builder.Services.AddMediatRConfiguration();

            var app = builder.Build();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBasicHealthChecks();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
