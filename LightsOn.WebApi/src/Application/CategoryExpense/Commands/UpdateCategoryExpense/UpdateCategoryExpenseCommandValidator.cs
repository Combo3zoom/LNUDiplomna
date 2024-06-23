namespace LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;

public class UpdateCategoryExpenseCommandValidator : AbstractValidator<UpdateCategoryExpenseCommand>
{
    public UpdateCategoryExpenseCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200);

    }
}