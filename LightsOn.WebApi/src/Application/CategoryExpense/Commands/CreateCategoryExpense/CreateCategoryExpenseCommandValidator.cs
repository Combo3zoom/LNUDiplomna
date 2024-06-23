namespace LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;

public class CreateCategoryExpenseCommandValidator : AbstractValidator<CreateCategoryExpenseCommand>
{
    public CreateCategoryExpenseCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}