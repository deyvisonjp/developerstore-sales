namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class ConfigurationExtensions
{
    public static void ApplyEnvVariables(this IConfiguration configuration)
    {
        var envVars = new Dictionary<string, string?>
        {
            ["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"),
            ["ConnectionStrings:MongoDb"] = Environment.GetEnvironmentVariable("MONGO_DB"),
            ["ConnectionStrings:Redis"] = Environment.GetEnvironmentVariable("REDIS_CONNECTION")
        };

        foreach (var kvp in envVars)
        {
            if (!string.IsNullOrEmpty(kvp.Value))
                (configuration as IConfigurationBuilder)?.AddInMemoryCollection(
                    new Dictionary<string, string?> { [kvp.Key] = kvp.Value }
                );
        }
    }
}
