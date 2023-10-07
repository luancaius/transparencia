using DepencyInjection;

namespace Presentation.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var resolver = new ResolveDependencies();
            
            var consoleApp = resolver.Resolve<ConsoleApp>();

            await consoleApp.ExecuteConsole();
        }
    }
}