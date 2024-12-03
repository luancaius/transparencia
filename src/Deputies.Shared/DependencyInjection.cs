using Deputies.Application.Ports.In;
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
            services.AddHttpClient();
            
            services.AddScoped<IGetDeputiesUseCase, GetDeputiesService>();
            services.AddScoped<IDeputyProvider, CamaraNewApiDeputyProvider>();
            return services;
        }
    }
}