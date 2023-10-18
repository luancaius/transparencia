namespace CacheDatabase.Interfaces;

public interface ICacheRepository
{
    Task<string> GetStringAsync(string key);
    Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
    Task<bool> KeyExistsAsync(string key);
    Task RemoveKeyAsync(string key);
}
