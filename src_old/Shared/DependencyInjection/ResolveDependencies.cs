using CacheDatabase.Interfaces;
using CacheDatabase.Repositories;
using DeputyUseCase.Implementation;
using DeputyUseCase.Interfaces;
using ExternalAPI.Implementation;
using ExternalAPI.Interfaces;
using Gateways.Implementation;
using Gateways.Interfaces;
using Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NonRelationalDatabase.Helpers;
using NonRelationalDatabase.Implementation;
using NonRelationalDatabase.Interfaces;
using RelationalDatabase.Database;
using RelationalDatabase.Interfaces;
using RelationalDatabase.Repositories;
using Repositories.Implementation;
using Repositories.Interfaces;
using Serilog;
using StackExchange.Redis;

namespace DependencyInjection;

public class ResolveDependencies
{
    private readonly ServiceProvider _serviceProvider;

    public ResolveDependencies()
    {
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        ConfigureServices(serviceCollection, configuration);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
    
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        #region Infrastructure
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        // MongoDB Configuration
        string mongoConnectionString = "mongodb://root:root@localhost:27017";
        string mongoDatabaseName = "congresso";
        try
        {
            services.AddSingleton<MongoDbHelper>(sp => new MongoDbHelper(mongoConnectionString, mongoDatabaseName));
        }
        catch (Exception e)
        {
            Console.WriteLine($"MongoDB is not running: ${e.Message}");
            throw;
        }
        
        services.AddTransient<INonRelationalDatabase, MongoDb>();
        
        string redisConnectionString = "localhost:6379";
        try
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);

            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddTransient<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            services.AddTransient<ICacheRepository, RedisCacheRepository>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Redis is not running: ${e.Message}");
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            services.AddSingleton<IMemoryCache>(memoryCache);
            services.AddTransient<ICacheRepository, MemoryCacheRepository>();
        }

        services.AddTransient<IBaseApi, BaseApi>();
        services.AddTransient<IDadosAbertosOldApi, DadosAbertosOldApi>();
        services.AddTransient<IDadosAbertosNewApi, DadosAbertosNewApi>();
        
        #endregion
        
        #region Gateways and Repositories
        
        services.AddTransient<IDeputiesGateway, DeputiesGateway>();
        services.AddTransient<IRepository, Repository>();
        services.AddTransient<IExpenseRepository, ExpenseRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        #endregion
        
        #region UseCase
        services.AddTransient<IDeputyUseCase, DeputyUseCaseImpl>();
        services.AddTransient<IDeputyApiUseCase, DeputyApiUseCaseImpl>();
        #endregion
        
        services.AddTransient<ILogger, CustomLogger>();
    }

    public T Resolve<T>()
    {
        return _serviceProvider.GetService<T>() ?? throw new InvalidOperationException();
    }
}
