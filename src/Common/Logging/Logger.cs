using Serilog;
using Serilog.Events;

namespace Logging;

public class CustomLogger : ILogger
{
    private readonly ILogger _logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {SourceContext} {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

    public void Write(LogEvent logEvent)
    {
        _logger.Write(logEvent);
    }
}