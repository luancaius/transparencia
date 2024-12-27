using Deputies.Adapter.Out.ExternalAPI;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deputies.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDeputiesSharedServices(this IServiceCollection services)
        {
            services.AddLogging();

            services.AddHttpClient();
            
            services.AddScoped<IGetDeputiesUseCase, GetDeputiesService>();
            services.AddScoped<IDeputyProvider, CamaraNewApiDeputyProvider>();
            return services;
        }
    }
}