﻿using System.Data.Common;
using CacheDatabase.Interfaces;
using CacheDatabase.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repositories.Mongo;
using Service.Services;
using Services.Interfaces;
using Services.Service;
using StackExchange.Redis;

namespace Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of DeputadoService from DI container
            var deputyService = serviceProvider.GetRequiredService<DeputyService>();

            bool running = true;
            System.Console.Write("Enter a command: ");
            string command = "e"; //Console.ReadLine().ToLower();

            System.Console.WriteLine($"Executing command {command}");
            switch (command)
            {
                case "a":
                    var deputies = await deputyService.GetAllDeputyRaw(57);
                    System.Console.WriteLine(deputies);
                    break;
                // case "b":
                //     await deputadoService.Api2_GetAllDeputados_SaveOnMongoDB();
                //     break;
                // case "c":
                //     await deputadoService.Api1_GetDeputadoDespesasByYear_SaveOnMongoDB(2023);
                //     break;
                // case "d":
                //     await deputadoService.Api2_GetListaPresencaDeputado_SaveOnMongoDB(2023);
                //     break;
                // case "e":
                //     await deputadoService.Api2_GetAllDeputados_SaveOnMongoDB();
                //     await deputadoService.Api2_GetListaPresencaDeputado_SaveOnMongoDB(2023);
                //     break;
                default:
                    System.Console.WriteLine("Invalid command. Please try again.");
                    break;
            }
            
            System.Console.WriteLine("Exiting the Command Console App. Goodbye!");
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            // MongoDB Configuration
            string mongoConnectionString = "mongodb://root:root@localhost:27017";
            string mongoDatabaseName = "congresso";

            string tableNameApi1 = "api1_deputados";
            string tableNameApi2 = "api2_deputados";
            string tableNameApi1Despesas = "api1_deputado_despesas";
            string tableNameApi2ListaPresenca = "api2_lista_presenca_deputados";
            
            services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
            services.AddTransient<Api1DeputadoMongoRepository>(sp => new Api1DeputadoMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi1));
            services.AddTransient<Api2DeputadoMongoRepository>(sp => new Api2DeputadoMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi2));
            services.AddTransient<Api1DeputadoDespesasMongoRepository>(sp => new Api1DeputadoDespesasMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi1Despesas));
            services.AddTransient<Api2DeputadoListaPresencaMongoRepository>(sp => new Api2DeputadoListaPresencaMongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi2ListaPresenca));



            // Redis Configuration
            string redisConnectionString = "localhost:6379";
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddTransient<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            services.AddTransient<IRedisCacheRepository, RedisCacheRepository>();

            // Services
            services.AddTransient<IDeputyService, DeputyService>();
            
        }

    }
}