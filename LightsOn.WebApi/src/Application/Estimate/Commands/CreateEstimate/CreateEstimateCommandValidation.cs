namespace LightsOn.Application.Estimate.Commands.CreateEstimate;

public class CreateEstimateCommandValidation : AbstractValidator<CreateEstimateCommand>
{
    public CreateEstimateCommandValidation()
    {
        RuleFor(estimate => estimate.CategoryExpense)
            .NotEmpty();
        
        RuleFor(estimate => estimate.Material)
            .NotEmpty();

        RuleFor(estimate => estimate.UsedMaterialsCount)
            .LessThanOrEqualTo(estimate => estimate.MaterialsCount);

    }
}