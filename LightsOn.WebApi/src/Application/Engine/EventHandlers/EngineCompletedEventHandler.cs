using LightsOn.Domain.Events.Engine;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Engine.EventHandlers;

public class EngineCompletedEventHandler : INotificationHandler<EngineCompletedEvent>
{
    private readonly ILogger<EngineCompletedEventHandler> _logger;

    public EngineCompletedEventHandler(ILogger<EngineCompletedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(EngineCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Engine completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}