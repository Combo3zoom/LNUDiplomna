namespace LightsOn.Application.ServiceDescription.Commands.CreateServiceDescription;

public class CreateServiceDescriptionCommandValidation : AbstractValidator<CreateServiceDescriptionCommand>
{
    public CreateServiceDescriptionCommandValidation()
    {
        RuleFor(s => s.HeaderText)
            .NotEmpty();
        
        RuleFor(s => s.MainText)
            .NotEmpty();
        
        RuleFor(s => s.LowerPriceLimit)
            .NotEmpty();
    }
}