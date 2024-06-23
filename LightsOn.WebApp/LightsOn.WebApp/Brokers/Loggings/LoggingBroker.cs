namespace LightsOn.WebApp.Brokers.Loggings;

public class LoggingBroker : ILoggingBroker
{
    private readonly ILogger _logger;

    public LoggingBroker(ILogger logger) => _logger = logger;

    public void LogCritical(Exception exception) =>
        _logger.LogCritical(exception, "{Message}", exception.Message);

    public void LogDebug(string message) => _logger.LogDebug(
        "{Message}", message);

    public void LogError(Exception exception) =>
        _logger.LogError(exception, "{Message}", exception.Message);

    public void LogInformation(string message) => _logger.LogInformation(
        "{Message}", message);
    public void LogTrace(string message) => _logger.LogTrace(
        "{Message}", message);
    public void LogWarning(string message) => _logger.LogWarning(
        "{Message}", message);
}