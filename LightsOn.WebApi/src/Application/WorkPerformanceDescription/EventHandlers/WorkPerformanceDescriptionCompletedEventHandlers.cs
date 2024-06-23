using LightsOn.Domain.Events.WorkPerformanceDescription;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.WorkPerformanceDescription.EventHandlers;

public class WorkPerformanceDescriptionCompletedEventHandlers : INotificationHandler<WorkPerformanceDescriptionCompletedEvent>
{
    private readonly ILogger<WorkPerformanceDescriptionCompletedEventHandlers> _logger;

    public WorkPerformanceDescriptionCompletedEventHandlers(ILogger<WorkPerformanceDescriptionCompletedEventHandlers> logger) => _logger = logger;

    public Task Handle(WorkPerformanceDescriptionCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("WorkPerformanceDescription completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}