using Deputies.Application.Ports.Out;
using Deputies.Application.Services;
using Deputies.ExternalAPI;
using Microsoft.Extensions.DependencyInjection;

namespace Deputies.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDeputiesSharedServices(this IServiceCollection services)
        {
            services.AddLogging();

            // Register application-specific services
            services.AddHttpClient<IExternalDeputyService, ExternalDeputyService>(); // Register the HttpClient
            services.AddTransient<DeputyService>();

            return services;
        }
    }
}