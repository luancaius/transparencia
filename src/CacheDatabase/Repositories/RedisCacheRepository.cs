using CacheDatabase.Interfaces;
using StackExchange.Redis;

namespace CacheDatabase.Repositories;

public class RedisCacheRepository : IRedisCacheRepository
{
    private readonly IDatabase _database;

    public RedisCacheRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<string> GetStringAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        await _database.StringSetAsync(key, value, expiry);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task RemoveKeyAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}