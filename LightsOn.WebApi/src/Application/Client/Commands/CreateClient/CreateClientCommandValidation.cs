namespace LightsOn.Application.Client.Commands.CreateClient;

public class CreateClientCommandValidation : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidation()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}