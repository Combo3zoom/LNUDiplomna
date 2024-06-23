using LightsOn.Domain.Events.Estimate;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Estimate.EventHandlers;

public class EstimateCompletedEventHandler : INotificationHandler<EstimateCompletedEvent>
{
    private readonly ILogger<EstimateCompletedEventHandler> _logger;

    public EstimateCompletedEventHandler(ILogger<EstimateCompletedEventHandler> logger) => _logger = logger;

    public Task Handle(EstimateCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Estimate completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}