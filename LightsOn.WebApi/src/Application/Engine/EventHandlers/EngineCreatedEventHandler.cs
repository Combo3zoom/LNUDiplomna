using LightsOn.Domain.Events.Engine;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Engine.EventHandlers;

public class EngineCreatedEventHandler : INotificationHandler<EngineCreatedEvent>
{
    private readonly ILogger<EngineCreatedEventHandler> _logger;

    public EngineCreatedEventHandler(ILogger<EngineCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(EngineCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Engine created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}