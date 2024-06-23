using LightsOn.Domain.Events.UnitMeasurement;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.UnitMeasurement.EventHandlers;

public class UnitMeasurementCreatedEventHandler : INotificationHandler<UnitMeasurementCreatedEvent>
{
    private readonly ILogger<UnitMeasurementCreatedEventHandler> _logger;

    public UnitMeasurementCreatedEventHandler(ILogger<UnitMeasurementCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(UnitMeasurementCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UnitMeasurement created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}