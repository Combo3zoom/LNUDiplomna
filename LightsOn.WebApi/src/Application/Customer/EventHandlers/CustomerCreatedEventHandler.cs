using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Customer;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Customer.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;
    private readonly ITelegramBot _telegramBot;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger, ITelegramBot telegramBot)
    {
        _logger = logger;
        _telegramBot = telegramBot;
    } 

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Customer created event: {DomainEvent}", notification.GetType().Name);

        var message = $"""
                       Нове замовлення:
                       *- Ім'я:* {notification.Customer.Name}
                       *- Телефон:* {notification.Customer.PhoneNumber}
                       *- Опис проблеми:* {notification.Customer.DescribeProblem}
                       """;
        
        await _telegramBot.SendMessageToAllowedUsers(message);
        
        _logger.LogInformation("Send telegram bot message event");
    }
}