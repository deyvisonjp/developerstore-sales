using Serilog;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var redisUrl = config.GetConnectionString("Redis") ?? "localhost:6379";

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            try
            {
                var uri = new Uri(redisUrl);
                string? password = null;

                if (!string.IsNullOrEmpty(uri.UserInfo) && uri.UserInfo.Contains(':'))
                    password = uri.UserInfo.Split(':')[1];

                var options = new ConfigurationOptions
                {
                    EndPoints = { { uri.Host, uri.Port } },
                    Password = password,
                    Ssl = uri.Scheme == "rediss",
                    AbortOnConnectFail = false,
                    ConnectRetry = 5,
                    ConnectTimeout = 10000,
                    DefaultDatabase = 0
                };

                Log.Information("Connecting to Redis at {Host}:{Port} with SSL={Ssl}", uri.Host, uri.Port, options.Ssl);
                return ConnectionMultiplexer.Connect(options);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to parse Redis URL or connect");
                throw;
            }
        });

        return services;
    }
}
