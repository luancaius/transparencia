using DependencyInjection;
using DeputyUseCase.Interfaces;

namespace Presentation.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try 
            {
                var resolver = new ResolveDependencies();
                var deputyUseCase = resolver.Resolve<IDeputyUseCase>();

                await ExecuteConsole(deputyUseCase);            
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
        
        public static async Task ExecuteConsole(IDeputyUseCase deputyUseCase)
        {
            string command = "a";

            System.Console.WriteLine($"Executing command {command}");
            switch (command)
            {
                case "a":
                    await deputyUseCase.GetAndStoreDeputiesDetailsInfo(2023);
                    break;
                case "b":
                    await deputyUseCase.GetAndStoreDeputiesExpenses(2023);
                    break;
                default:
                    System.Console.WriteLine("Invalid command. Please try again.");
                    break;
            }

            System.Console.WriteLine("Exiting the Command Console App. Goodbye!");
        }
    }
}