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
            services.AddSingleton<IMongoRepository<Api1DeputadoDtoMongo>, Api1MongoRepository>(sp =>
                new Api1MongoRepository("mongodb://root:root@localhost:27017", "congresso"));
            services.AddSingleton<IMongoRepository<Api2DeputadoDtoMongo>, Api2MongoRepository>(sp =>
                new Api2MongoRepository("mongodb://root:root@localhost:27017", "congresso"));
            services.AddTransient<Api1RestService>();
            services.AddTransient<Api2SoapService>();
            services.AddTransient<DeputadoService>();
        }
    }
}