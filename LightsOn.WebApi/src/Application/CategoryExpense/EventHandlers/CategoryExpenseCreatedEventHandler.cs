using LightsOn.Domain.Events.CategoryExpense;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.CategoryExpense.EventHandlers;

public class CategoryExpenseCreatedEventHandler : INotificationHandler<CategoryExpenseCreatedEvent>
{
    private readonly ILogger<CategoryExpenseCreatedEventHandler> _logger;

    public CategoryExpenseCreatedEventHandler(ILogger<CategoryExpenseCreatedEventHandler> logger)
        => _logger = logger;
    
    public Task Handle(CategoryExpenseCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CategoryExpense created domain event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}