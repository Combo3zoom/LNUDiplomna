namespace LightsOn.Application.Estimate.Commands.UpdateEstimate;

public class UpdateEstimateCommandValidation : AbstractValidator<UpdateEstimateCommand>
{
    public UpdateEstimateCommandValidation()
    {
        RuleFor(estimate => estimate.UsedMaterialsCount)
            .LessThanOrEqualTo(estimate => estimate.MaterialsCount);
    }
}