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
            string command = "a";

            System.Console.WriteLine($"Executing command {command}");
            switch (command)
            {
                case "a":
                    var deputies = await _deputyService.GetAllDeputyRaw(57);
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
    }
}