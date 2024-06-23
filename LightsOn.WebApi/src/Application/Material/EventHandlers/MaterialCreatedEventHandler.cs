using LightsOn.Domain.Events.Material;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Material.EventHandlers;

public class MaterialCreatedEventHandler : INotificationHandler<MaterialCreatedEvent>
{
    private readonly ILogger<MaterialCreatedEventHandler> _logger;

    public MaterialCreatedEventHandler(ILogger<MaterialCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(MaterialCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Material created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}