using Deputies.Application.Ports.In;
using Deputies.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Deputies.MainConsole;

public enum Command
{
    GetAllDeputiesInfo,
    GetDeputiesExpenses
}

public static class MainConsole
{
    public static async Task<int> Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("No command provided. Please specify a command.");
            return 1; // Return non-zero to indicate an error
        }

        if (!Enum.TryParse(args[0], true, out Command command))
        {
            Console.WriteLine($"Invalid command: {args[0]}");
            return 1; // Return non-zero to indicate an error
        }

        var serviceProvider = BuildServiceProvider();
        var deputiesUseCase = serviceProvider.GetRequiredService<IGetDeputiesUseCase>();

        try
        {
            switch (command)
            {
                case Command.GetAllDeputiesInfo:
                    if (args.Length < 2 || !int.TryParse(args[1], out int yearDeputies))
                    {
                        Console.WriteLine("Please provide a valid year for GetAllDeputiesInfo.");
                        return 1; // Return non-zero to indicate an error
                    }

                    await deputiesUseCase.ProcessDeputiesAsync(yearDeputies);

                    break;

                default:
                    Console.WriteLine($"Command not implemented: {command}");
                    return 1; // Return non-zero to indicate an error
            }

            return 0; // Return zero to indicate success
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return 1; // Return non-zero to indicate an error
        }
    }

    private static ServiceProvider BuildServiceProvider()
    {
        return new ServiceCollection()
            .AddDeputiesSharedServices() // Use shared dependency injection setup
            .BuildServiceProvider();
    }
}