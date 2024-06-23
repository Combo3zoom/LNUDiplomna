namespace LightsOn.Application.Estimate.Queries.GetByIdEstimate;

public class GetByIdEstimateValidation : AbstractValidator<GetByIdEstimate>
{
    public GetByIdEstimateValidation()
    {
        RuleFor(estimate => estimate.Id)
            .NotEmpty();
    }
}