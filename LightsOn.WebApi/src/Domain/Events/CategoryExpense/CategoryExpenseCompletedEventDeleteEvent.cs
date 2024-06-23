namespace LightsOn.Domain.Events.CategoryExpense;

public class CategoryExpenseDeleteEvent : BaseEvent
{
    public CategoryExpenseDeleteEvent(Entities.CategoryExpense categoryExpense)
    {
        CategoryExpense = categoryExpense;
    }

    public Entities.CategoryExpense CategoryExpense { get; }
}