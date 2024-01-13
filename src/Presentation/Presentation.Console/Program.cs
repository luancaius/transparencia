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
                var _personService = resolver.Resolve<IPersonService>();

                await ExecuteConsole(_deputyService, _personService);            
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
        
        public static async Task ExecuteConsole(IDeputyService _deputyService, IPersonService _personService)
        {
            string command = "g";

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
                case "bb":
                    await _deputyService.RefreshDeputyDetails(2022);
                    break;
                case "c":
                    await _deputyService.RefreshAllMongoDb(2023);
                    break;
                case "d":
                    await _deputyService.RefreshNewApi(2022);
                    break;
                case "e":
                    await _deputyService.RefreshOldApi(2022);
                    break;
                case "f":
                    await _deputyService.RefreshRelationalDbFromNonRelationalDb(2022);
                    break;
                case "g":
                    await _deputyService.RefreshDeputyDetailRelationalDb();
                    break;
                default:
                    System.Console.WriteLine("Invalid command. Please try again.");
                    break;
            }

            System.Console.WriteLine("Exiting the Command Console App. Goodbye!");
        }
    }
}