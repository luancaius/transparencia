using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.JsonEntity;
using Service.Services;

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
            var deputadoService = serviceProvider.GetRequiredService<DeputadoService>();

            bool running = true;
            System.Console.Write("Enter a command: ");
            string command = "b"; //Console.ReadLine().ToLower();

            System.Console.WriteLine($"Executing command {command}");
            switch (command)
            {
                case "a":
                    await deputadoService.Api1_GetAllDeputados_SaveOnMongoDB();
                    break;
                case "b":
                    await deputadoService.Api2_GetAllDeputados_SaveOnMongoDB();
                    break;
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
            services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
            services.AddTransient<Api1MongoRepository>(sp => new Api1MongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi1));
            services.AddTransient<Api2MongoRepository>(sp => new Api2MongoRepository(sp.GetRequiredService<MongoDbContext>(), tableNameApi2));

            services.AddTransient<Api1RestService>();
            services.AddTransient<Api2SoapService>();
            services.AddTransient<DeputadoService>();
        }
    }
}