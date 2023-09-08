using Cache.Interceptors;
using Castle.DynamicProxy;
using StackExchange.Redis;

namespace Cache.Utilities;

public class ProxyUtility
{
    public T CreateProxyWithCache<T>(T instance, IDatabase redisDb) where T : class
    {
        var generator = new ProxyGenerator();
        return generator.CreateInterfaceProxyWithTarget(instance, new RedisCacheInterceptor(redisDb));
    }

}