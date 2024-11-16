namespace Deputies.MainConsole;

public enum Command
{
    GetAllDeputiesInfo,
    GetDeputiesExpenses
}

public static class MainConsole
{
    public static int Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("No command provided. Please specify a command.");
            return 1; // Return non-zero to indicate an error
        }

        // Attempt to parse the command from the first argument
        if (!Enum.TryParse(args[0], true, out Command command))
        {
            Console.WriteLine($"Invalid command: {args[0]}");
            return 1; // Return non-zero to indicate an error
        }

        try
        {
            switch (command)
            {
                case Command.GetAllDeputiesInfo:
                    break;

                case Command.GetDeputiesExpenses:
                    // Check if the year argument is provided
                    if (args.Length < 2 || !int.TryParse(args[1], out int year))
                    {
                        Console.WriteLine("Please provide a valid year for GetDeputiesExpenses.");
                        return 1; // Return non-zero to indicate an error
                    }

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
}