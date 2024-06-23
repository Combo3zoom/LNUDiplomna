namespace LightsOn.Application.UnitMeasurement.Commands.UpdateUnitMeasurement;

public class UpdateUnitMeasurementCommandHandlerValidation : AbstractValidator<UpdateUnitMeasurementCommand>
{
    public UpdateUnitMeasurementCommandHandlerValidation()
    {
        RuleFor(material => material.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}