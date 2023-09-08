using System.Reflection;
using StackExchange.Redis;

namespace Repository.Repositories;

[AttributeUsage(AttributeTargets.Method)]
public class RedisCacheAttribute : Attribute
{
    private static IDatabase _redisDb;

    public int ExpiryMinutes { get; set; } = 60; 
    public string KeyPattern { get; set; } 

    public RedisCacheAttribute()
    {
        if (_redisDb == null)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"); 
            _redisDb = redis.GetDatabase();
        }
    }

    // ... other methods remain the same ...

    private string GenerateKeyForMethod(MethodBase method, object[] arguments)
    {
        // If a pattern is defined, use it to generate the key
        if (!string.IsNullOrEmpty(KeyPattern))
        {
            return string.Format(KeyPattern, arguments);
        }

        // Else, use the default method name as the key
        return $"CACHE:{method.Name}";
    }
}
