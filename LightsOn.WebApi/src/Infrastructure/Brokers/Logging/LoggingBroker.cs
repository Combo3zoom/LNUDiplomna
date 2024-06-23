using Microsoft.Extensions.Logging;

namespace LightsOn.WebApi.Logging;

public class LoggingBroker : ILoggingBroker
{
    private readonly ILogger _logger;

    public LoggingBroker(ILogger logger) => _logger = logger;

    public void LogInformation(string message)
        => this._logger.LogInformation(message);

    public void LogTrace(string message)
        => this._logger.LogTrace(message);

    public void LogDebug(string message)
        => this._logger.LogDebug(message);

    public void LogWarning(string message)
        => this._logger.LogWarning(message);

    public void LogError(Exception exception)
        => this._logger.LogError(exception, exception.Message);

    public void LogCritical(Exception exception)
        => this._logger.LogCritical(exception, exception.Message);
}