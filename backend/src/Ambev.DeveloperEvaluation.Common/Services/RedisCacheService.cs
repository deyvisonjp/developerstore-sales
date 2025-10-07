using Ambev.DeveloperEvaluation.Common.Services;

namespace Ambev.DeveloperEvaluation.Common.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly StackExchange.Redis.IConnectionMultiplexer _redis;
        public RedisCacheService(StackExchange.Redis.IConnectionMultiplexer redis) => _redis = redis;

        public async Task<T?> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, System.Text.Json.JsonSerializer.Serialize(value), expiry);
        }

        public async Task RemoveAsync(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}