using Serilog;

namespace Common.Logging;

public class LoggerManager : ILoggerManager
{
    private static readonly ILogger logger = Log.Logger;

    public void LogDebug(string message) => logger.Debug(message);
    public void LogError(string message) => logger.Error(message);
    public void LogInfo(string message) => logger.Information(message);
    public void LogWarn(string message) => logger.Warning(message);
}