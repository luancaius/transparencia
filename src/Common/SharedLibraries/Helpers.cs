using Serilog;

namespace SharedLibraries;

public static class GenericHelpers
{
    public static readonly ILogger _Logger;
    public static bool HasEntitySameValues<T>(T entity1, T entity2)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value1 = property.GetValue(entity1);
            var value2 = property.GetValue(entity2);
            if (value1 != value2)
            {
                _Logger.Information("Entity has different values: $property.Name $value1 $value2");
                return false;
            }
        }

        return true;
    }
}