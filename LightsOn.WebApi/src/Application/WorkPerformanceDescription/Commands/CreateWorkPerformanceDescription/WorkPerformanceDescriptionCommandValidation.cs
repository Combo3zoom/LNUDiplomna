namespace LightsOn.Application.WorkPerformanceDescription.Commands.CreateWorkPerformanceDescription;

public class WorkPerformanceDescriptionCommandValidation : AbstractValidator<CreateWorkPerformanceDescriptionCommand>
{
    public WorkPerformanceDescriptionCommandValidation()
    {
        RuleFor(w => w.Client)
            .NotEmpty();
        
        RuleFor(w => w.PowerEquipment)
            .NotEmpty();
        
        RuleFor(w => w.Engine)
            .NotEmpty();
    }
}