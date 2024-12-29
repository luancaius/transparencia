using Deputies.Adapter.Out.ExternalAPI;
using Deputies.Adapter.Out.EFCoreSqlServer;
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
            // cross-cutting
            services.AddLogging();
            services.AddHttpClient();

            // application
            services.AddScoped<IGetDeputiesUseCase, GetDeputiesService>();

            // adapters
            services.AddScoped<IDeputyProvider, CamaraNewApiDeputyProvider>();
            services.AddScoped<IDeputyRepository, DeputyRepository>();

            return services;
        }
    }
}