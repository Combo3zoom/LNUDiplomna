namespace LightsOn.Domain.Events.CategoryExpense;

public class CategoryExpenseCreatedEvent : BaseEvent
{
    public CategoryExpenseCreatedEvent(Entities.CategoryExpense categoryExpense)
    {
        CategoryExpense = categoryExpense;
    }

    public Entities.CategoryExpense CategoryExpense { get; }
}