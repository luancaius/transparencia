using Service.Interfaces;
using StackExchange.Redis;

namespace Service.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IDatabase database)
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
