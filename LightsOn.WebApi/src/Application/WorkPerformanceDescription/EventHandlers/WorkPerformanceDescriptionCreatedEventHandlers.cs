using LightsOn.Domain.Events.WorkPerformanceDescription;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.WorkPerformanceDescription.EventHandlers;

public class WorkPerformanceDescriptionCreatedEventHandlers : INotificationHandler<WorkPerformanceDescriptionCreatedEvent>
{
    private readonly ILogger<WorkPerformanceDescriptionCompletedEventHandlers> _logger;

    public WorkPerformanceDescriptionCreatedEventHandlers(ILogger<WorkPerformanceDescriptionCompletedEventHandlers> logger)
        => _logger = logger;

    public Task Handle(WorkPerformanceDescriptionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("WorkPerformanceDescription created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}