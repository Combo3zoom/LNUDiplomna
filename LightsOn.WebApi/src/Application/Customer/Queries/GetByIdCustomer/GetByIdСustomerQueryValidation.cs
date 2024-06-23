namespace LightsOn.Application.Customer.Queries.GetByIdCustomer;

public class GetByIdСustomerQueryValidation : AbstractValidator<GetByIdCustomer>
{
    public GetByIdСustomerQueryValidation()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}