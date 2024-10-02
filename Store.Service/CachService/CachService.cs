using StackExchange.Redis;
using System.Text.Json;

namespace Store.Service.CachService
{
    public class CachService : ICachService
    {
        private readonly IDatabase _database;

        public CachService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCachResponseAsync(string key)
        {
            var cachedResponse = await _database.StringGetAsync(key);
            if(cachedResponse.IsNullOrEmpty)
                return null;
            return cachedResponse.ToString();
        }

        public async Task SetCachResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponseon = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(key, serializedResponseon, timeToLive);

        }
    }
}
