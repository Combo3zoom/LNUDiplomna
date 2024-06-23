namespace LightsOn.Application.WorkPerformanceDescription.Commands.UpdateWorkPerformanceDescription;

public class UpdateWorkPerformanceDescriptionCommandValidation : AbstractValidator<UpdateWorkPerformanceCommandDescription>
{
    public UpdateWorkPerformanceDescriptionCommandValidation()
    {
        RuleFor(w => w.Client)
            .NotEmpty();
        
        RuleFor(w => w.PowerEquipment)
            .NotEmpty();
        
        RuleFor(w => w.Engine)
            .NotEmpty();
    }
}