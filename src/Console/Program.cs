using Repository;
using Service.Services;

namespace Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var jsonRepository = new JsonRepository("mongodb://root:root@localhost:27017", "congresso");
            var api1Service = new Api1RestService();
            var api2Service = new Api2SoapService();
            
            bool running = true;
            DeputadoService deputadoService = new DeputadoService(jsonRepository, api1Service, api2Service);
            
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
    }
}