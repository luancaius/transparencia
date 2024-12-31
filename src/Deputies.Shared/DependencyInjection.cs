using Deputies.Adapter.Out.ExternalAPI;
using Deputies.Adapter.Out.EFCoreSqlServer;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Deputies.Shared
{
    public static class DependencyInjection
    {
        // Note the extra 'IConfiguration configuration' parameter:
        public static IServiceCollection AddDeputiesSharedServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // cross-cutting
            services.AddLogging();
            services.AddHttpClient();

            // application
            services.AddScoped<IGetDeputiesUseCase, GetDeputiesService>();

            // adapters
            services.AddScoped<IDeputyProvider, CamaraNewApiDeputyProvider>();
            services.AddScoped<IDeputyRepository, DeputyRepository>();

            // EF Core DbContext using SQL Server
            services.AddDbContext<DeputiesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DeputiesSqlServerConnection")));

            return services;
        }
    }
}