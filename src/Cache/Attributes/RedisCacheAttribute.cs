using System.Reflection;

namespace Cache.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class RedisCacheAttribute : Attribute
{
    public int ExpiryMinutes { get; set; } = 60;

    public string GenerateKeyForMethod(MethodInfo method, object[] args)
    {
        string methodName = method.Name;
        string argsString = string.Join(",", args.Select(a => a?.ToString() ?? "NULL"));
        return $"CACHE:{methodName}:{argsString}";
    }
}
