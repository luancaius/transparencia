using DependencyInjection;
using Services.Interfaces;

namespace Presentation.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try 
            {
                var resolver = new ResolveDependencies();
            
                var _deputyService = resolver.Resolve<IDeputyService>();

                await ExecuteConsole(_deputyService);            
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
        
        public static async Task ExecuteConsole(IDeputyService _deputyService)
        {
            string command = "c";

            System.Console.WriteLine($"Executing command {command}");
            switch (command)
            {
                case "a":
                    var deputies = await _deputyService.GetDeputiesListExternalApi(57);
                    System.Console.WriteLine(deputies);
                    break;
                case "b":
                    await _deputyService.GetDeputiesDetailListExternalApi(57);
                    break;
                case "c":
                    await _deputyService.RefreshDatabase(57, 2022);
                    break;
                default:
                    System.Console.WriteLine("Invalid command. Please try again.");
                    break;
            }

            System.Console.WriteLine("Exiting the Command Console App. Goodbye!");
        }
    }
}