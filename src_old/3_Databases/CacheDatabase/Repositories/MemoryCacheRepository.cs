using CacheDatabase.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDatabase.Repositories;

public class MemoryCacheRepository : ICacheRepository
{
    private readonly IMemoryCache memoryCache;

    public MemoryCacheRepository(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public async Task<string> GetStringAsync(string key)
    {
        if (memoryCache.TryGetValue(key, out string cachedValue))
        {
            return cachedValue;
        }
            
        return null; // Key not found in cache
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(30) // Default cache duration
        };
            
        memoryCache.Set(key, value, cacheEntryOptions);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        return memoryCache.TryGetValue(key, out _);
    }

    public async Task RemoveKeyAsync(string key)
    {
        memoryCache.Remove(key);
    }
}
