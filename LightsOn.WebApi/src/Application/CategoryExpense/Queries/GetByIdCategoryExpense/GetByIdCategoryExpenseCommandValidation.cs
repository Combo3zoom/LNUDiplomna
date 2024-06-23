namespace LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;

public class GetByIdCategoryExpenseCommandValidation : AbstractValidator<GetByIdCategoryExpenseQuery>
{
    public GetByIdCategoryExpenseCommandValidation()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}