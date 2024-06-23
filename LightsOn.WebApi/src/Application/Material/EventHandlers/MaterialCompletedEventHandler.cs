using LightsOn.Domain.Events.Material;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Material.EventHandlers;

public class MaterialCompletedEventHandler : INotificationHandler<MaterialCompletedEvent>
{
    private readonly ILogger<MaterialCompletedEventHandler> _logger;

    public MaterialCompletedEventHandler(ILogger<MaterialCompletedEventHandler> logger) => _logger = logger;

    public Task Handle(MaterialCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Material completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}