namespace LightsOn.Domain.Events.CategoryExpense;

public class CategoryExpenseCompletedEvent : BaseEvent
{
    public CategoryExpenseCompletedEvent(Entities.CategoryExpense categoryExpense)
    {
        CategoryExpense = categoryExpense;
    }

    public Entities.CategoryExpense CategoryExpense { get; }
}