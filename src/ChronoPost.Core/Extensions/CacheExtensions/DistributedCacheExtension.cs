using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ChronoPost.Core.Extensions.CacheExtensions;

/// <summary>
/// Defines extension methods for <c>IDistributedCache interface
/// </summary>
public static class DistributedCacheExtension
{
    /// <summary>
    /// Set cache with given key, data and cacheTime
    /// </summary>
    /// <param name="cache"><inheritdoc cref="IDistributedCache"/></param>
    /// <param name="key">Represents unique key for cache data.</param>
    /// <param name="data">Represents data for cache.</param>
    /// <param name="cacheTime">Represents expiration time relative to now.</param>
    /// <typeparam name="T">Represents type of the Cache Data</typeparam>
    public static async Task SetCacheAsync<T>(this IDistributedCache cache, string key, T data, TimeSpan cacheTime, CancellationToken cancellationToken = default)
    {
        var content = JsonSerializer.Serialize(data);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheTime
        };
        await cache.SetStringAsync(key, content, options, token: cancellationToken);
    }


    public static async Task<T> GetFromCache<T>(this IDistributedCache cache, string key, CancellationToken cancellationToken = default)
    {
        var content = await cache.GetStringAsync(key, cancellationToken);

        var result = JsonSerializer.Deserialize<T>(content);

        return result;
    }
    
}