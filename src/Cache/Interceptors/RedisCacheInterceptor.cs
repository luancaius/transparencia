using Castle.DynamicProxy;
using StackExchange.Redis;
using System.Text.Json;
using Cache.Attributes;

namespace Cache.Interceptors;

public class RedisCacheInterceptor : IInterceptor
{
    private readonly IDatabase _redisDb;

    public RedisCacheInterceptor(IDatabase redisDb)
    {
        _redisDb = redisDb;
    }

    public void Intercept(IInvocation invocation)
    {
        var cacheAttribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(RedisCacheAttribute), false);
        if (cacheAttribute.Length == 0)
        {
            invocation.Proceed();
            return;
        }

        var attribute = (RedisCacheAttribute)cacheAttribute[0];
        string cacheKey = attribute.GenerateKeyForMethod(invocation.Method, invocation.Arguments);

        string cachedData = _redisDb.StringGet(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var returnType = invocation.Method.ReturnType;
            invocation.ReturnValue = JsonSerializer.Deserialize(cachedData, returnType);
            return;
        }

        invocation.Proceed();

        if (invocation.ReturnValue != null)
        {
            _redisDb.StringSet(cacheKey, JsonSerializer.Serialize(invocation.ReturnValue), TimeSpan.FromMinutes(attribute.ExpiryMinutes));
        }
    }
}
