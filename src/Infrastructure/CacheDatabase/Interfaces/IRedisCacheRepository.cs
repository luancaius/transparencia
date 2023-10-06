namespace CacheDatabase.Interfaces;

public interface IRedisCacheRepository
{
    Task<string> GetStringAsync(string key);
    Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
    Task<bool> KeyExistsAsync(string key);
    Task RemoveKeyAsync(string key);
}