using LightsOn.Domain.Events.UnitMeasurement;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.UnitMeasurement.EventHandlers;

public class UnitMeasurementCompletedEventHandler : INotificationHandler<UnitMeasurementCompletedEvent>
{
    private readonly ILogger<UnitMeasurementCompletedEventHandler> _logger;

    public UnitMeasurementCompletedEventHandler(ILogger<UnitMeasurementCompletedEventHandler> logger) => _logger = logger;

    public Task Handle(UnitMeasurementCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UnitMeasurement completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}