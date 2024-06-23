using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Engine.Commands.UpdateEngine;

public class UpdateEngineCommandValidation : AbstractValidator<UpdateEngineCommand>
{
    private readonly IApplicationDbContext _context;
    
    public UpdateEngineCommandValidation(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(engine => engine.Name)
            .NotEmpty()
            .MaximumLength(200).WithMessage("Name should be contain less 200 symbols");

        RuleFor(engine => engine.SerialNumber)
            .NotEmpty()
            .MaximumLength(200);
    }


}