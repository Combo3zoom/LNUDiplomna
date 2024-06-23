using LightsOn.Domain.Events.Client;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Client.EventHandlers;

public class ClientCreatedEventHandler : INotificationHandler<ClientCreatedEvent>
{
    private readonly ILogger<ClientCreatedEventHandler> _logger;

    public ClientCreatedEventHandler(ILogger<ClientCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Client created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}