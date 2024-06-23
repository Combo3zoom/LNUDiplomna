using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Client;
using LightsOn.Domain.Events.Customer;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Customer.EventHandlers;

public class CustomerCompletedEventHandler : INotificationHandler<CustomerCompletedEvent>
{
    private readonly ILogger<CustomerCompletedEventHandler> _logger;

    public CustomerCompletedEventHandler(ILogger<CustomerCompletedEventHandler> logger)
    {
        _logger = logger;
    } 

    public Task Handle(CustomerCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Customer completed event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
    
}