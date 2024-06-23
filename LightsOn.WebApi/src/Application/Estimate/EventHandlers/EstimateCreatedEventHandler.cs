using LightsOn.Domain.Events.Estimate;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Estimate.EventHandlers;

public class EstimateCreatedEventHandler : INotificationHandler<EstimateCreatedEvent>
{
    private readonly ILogger<EstimateCreatedEventHandler> _logger;

    public EstimateCreatedEventHandler(ILogger<EstimateCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(EstimateCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Engine created event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}