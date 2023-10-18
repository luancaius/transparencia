using CacheDatabase.Interfaces;
using CacheDatabase.Repositories;
using ExternalAPI.Implementation;
using ExternalAPI.Interfaces;
using Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Implementation;
using Repositories.Interfaces;
using Serilog;
using Services.Interfaces;
using Services.Service;
using StackExchange.Redis;

namespace DependencyInjection;

public class ResolveDependencies
{
    private readonly ServiceProvider _serviceProvider;

    public ResolveDependencies()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        #region Infrastructure
        
        // MongoDB Configuration
        // string mongoConnectionString = "mongodb://root:root@localhost:27017";
        // string mongoDatabaseName = "congresso";
        //
        // string tableNameApi1 = "api1_deputados";
        // string tableNameApi2 = "api2_deputados";
        // string tableNameApi1Despesas = "api1_deputado_despesas";
        // string tableNameApi2ListaPresenca = "api2_lista_presenca_deputados";
        //     
        // services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
        // services.AddTransient<Api1DeputadoMongoRepository>(sp => new Api1DeputadoMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi1));
        // services.AddTransient<Api2DeputadoMongoRepository>(sp => new Api2DeputadoMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi2));
        // services.AddTransient<Api1DeputadoDespesasMongoRepository>(sp => new Api1DeputadoDespesasMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi1Despesas));
        // services.AddTransient<Api2DeputadoListaPresencaMongoRepository>(sp => new Api2DeputadoListaPresencaMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi2ListaPresenca));
        
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
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            services.AddSingleton<IMemoryCache>(memoryCache);
            services.AddTransient<IDatabase>(_ => null); // No Redis, so provide a null database
            services.AddTransient<ICacheRepository, MemoryCacheRepository>();
        }

        services.AddTransient<IBaseApi, BaseApi>();
        services.AddTransient<IDadosAbertosOldApi, DadosAbertosOldApi>();
        services.AddTransient<IDadosAbertosNewApi, DadosAbertosNewApi>();
        
        services.AddTransient<ISearchDeputyRepository, SearchDeputyRepository>();
        services.AddTransient<IDeputyRepository, DeputyRepository>();
        #endregion

        #region Application
        services.AddTransient<IDeputyService, DeputyService>();       
        #endregion
        
        services.AddTransient<ILogger, CustomLogger>();
    }

    public T Resolve<T>()
    {
        return _serviceProvider.GetService<T>() ?? throw new InvalidOperationException();
    }
}
