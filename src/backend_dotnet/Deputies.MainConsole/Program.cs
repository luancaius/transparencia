using Deputies.Application.Ports.In;
using Deputies.Shared;

namespace Deputies.MainConsole;

public enum Command
{
    GetAllDeputiesInfo,
    GetDeputiesExpensesCurrentMonth,
    GetDeputiesExpensesByYearAndMonth,
    GetDeputiesExpensesYear
}

public static class MainConsole
{
    public static async Task<int> Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("No command provided. Please specify a command.");
            return 1;
        }

        if (!Enum.TryParse(args[0], true, out Command command))
        {
            Console.WriteLine($"Invalid command: {args[0]}");
            return 1;
        }

        var resolver = new ResolveDependencies();
        var deputiesUseCase = resolver.Resolve<IGetDeputiesUseCase>();

        try
        {
            switch (command)
            {
                case Command.GetAllDeputiesInfo:
                    if (args.Length < 2 || !int.TryParse(args[1], out int yearDeputies))
                    {
                        Console.WriteLine("Please provide a valid year for GetAllDeputiesInfo.");
                        return 1;
                    }

                    await deputiesUseCase.ProcessDeputiesAsync(yearDeputies);
                    break;
                case Command.GetDeputiesExpensesCurrentMonth:
                    await deputiesUseCase.ProcessDeputiesExpensesCurrentMonthAsync();
                    break;
                case Command.GetDeputiesExpensesByYearAndMonth:
                    if (args.Length < 3 || !int.TryParse(args[1], out int yearDeputies3) || !int.TryParse(args[1], out int month))
                    {
                        Console.WriteLine("Please provide a valid year/month for GetAllDeputiesExpenses");
                        return 1;
                    }
                    await deputiesUseCase.ProcessDeputiesExpensesByMonthAndYearAsync(yearDeputies3, month);
                    break;
                case Command.GetDeputiesExpensesYear:
                    if (args.Length < 2 || !int.TryParse(args[1], out int yearDeputies2))
                    {
                        Console.WriteLine("Please provide a valid year for GetAllDeputiesExpenses");
                        return 1;
                    }

                    await deputiesUseCase.ProcessDeputiesExpensesByYearAsync(yearDeputies2);
                    break;
                default:
                    Console.WriteLine($"Command not implemented: {command}");
                    return 1; 
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return 1;
        }
    }
}
