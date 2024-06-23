using LightsOn.Domain.Events.Client;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Client.EventHandlers;

public class ClientCompletedEventHandler : INotificationHandler<ClientCompletedEvent>
{
    private readonly ILogger<ClientCompletedEventHandler> _logger;

    public ClientCompletedEventHandler(ILogger<ClientCompletedEventHandler> logger) => _logger = logger;

    public Task Handle(ClientCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Client completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}