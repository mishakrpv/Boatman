using System.Text.Json;
using Boatman.Caching.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Boatman.Caching.Implementations.Redis;

public class RedisCache : ICache
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _config;

    public RedisCache(IDistributedCache cache,
        IConfiguration config)
    {
        _cache = cache;
        _config = config;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var valueAsString = await _cache.GetStringAsync(key);

        if (valueAsString != null)
        {
            return JsonSerializer.Deserialize<T>(valueAsString);
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var valueAsString = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(key, valueAsString, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(
                double.Parse(_config["CacheExpirationRelativeToNowInHours"] ?? "48"))
        });
    }

    public async Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RefreshAsync(key);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(key);
    }
}