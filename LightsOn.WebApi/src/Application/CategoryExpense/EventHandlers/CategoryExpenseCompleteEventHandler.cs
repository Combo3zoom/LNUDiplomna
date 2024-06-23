using LightsOn.Domain.Events.CategoryExpense;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.CategoryExpense.EventHandlers;

public class CategoryExpenseCompleteEventHandler : INotificationHandler<CategoryExpenseCompletedEvent>
{
    private readonly ILogger<CategoryExpenseCompleteEventHandler> _logger;

    public CategoryExpenseCompleteEventHandler(ILogger<CategoryExpenseCompleteEventHandler> logger)
        => _logger = logger;

    public Task Handle(CategoryExpenseCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CategoryExpense completed domain event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}