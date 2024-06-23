using LightsOn.Domain.Events.PowerEquipment;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.PowerEquipment.EventHandlers;

public class PowerEquipmentCreatedEventHandler : INotificationHandler<PowerEquipmentCreatedEvent>
{
    private readonly ILogger<PowerEquipmentCreatedEventHandler> _logger;

    public PowerEquipmentCreatedEventHandler(ILogger<PowerEquipmentCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(PowerEquipmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PowerEquipment created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}