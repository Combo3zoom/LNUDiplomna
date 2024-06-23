using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Engine.Commands.CreateEngine;

public class CreateEngineCommandValidation : AbstractValidator<CreateEngineCommand>
{
    public CreateEngineCommandValidation()
    {
        RuleFor(engine => engine.Name)
            .NotEmpty()
            .MaximumLength(200).WithMessage("Name should be contain less 200 symbols");

        RuleFor(engine => engine.SerialNumber)
            .NotEmpty()
            .MaximumLength(200);
    }
}