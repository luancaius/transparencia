using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Deputies.Adapter.Out.EFCoreSqlServer;
using Deputies.Adapter.Out.ExternalAPI;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Application.Services;

namespace Deputies.Shared;

public class ResolveDependencies
{
    private readonly IServiceProvider _serviceProvider;

    public ResolveDependencies()
    {
        var services = new ServiceCollection();
            
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) 
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
            
        AddDeputiesSharedServices(services, configuration);

        _serviceProvider = services.BuildServiceProvider();
    }

    private static void AddDeputiesSharedServices(
        IServiceCollection services,
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
    }

    public T Resolve<T>() where T : notnull
        => _serviceProvider.GetRequiredService<T>();
}