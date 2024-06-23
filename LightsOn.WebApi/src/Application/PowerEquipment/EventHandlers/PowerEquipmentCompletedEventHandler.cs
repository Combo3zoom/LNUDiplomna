using LightsOn.Domain.Events.PowerEquipment;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.PowerEquipment.EventHandlers;

public class PowerEquipmentCompletedEventHandler : INotificationHandler<PowerEquipmentCompletedEvent>
{
    private readonly ILogger<PowerEquipmentCompletedEventHandler> _logger;

    public PowerEquipmentCompletedEventHandler(ILogger<PowerEquipmentCompletedEventHandler> logger) => _logger = logger;

    public Task Handle(PowerEquipmentCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PowerEquipment completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}