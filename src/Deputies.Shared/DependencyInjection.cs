using Microsoft.Extensions.DependencyInjection;

namespace Deputies.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddDeputiesSharedServices(this IServiceCollection services)
    {
        

        services.AddLogging();

        return services;
    }
}
