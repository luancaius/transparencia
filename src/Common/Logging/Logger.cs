using Serilog;
using Serilog.Events;

namespace Logging;

public class Logger
{
    public Logger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {SourceContext} {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }
    public void Write(LogEvent logEvent)
    {
        throw new NotImplementedException();
    }
}