namespace LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;

public class CreateUnitMeasurementCommandValidation : AbstractValidator<CreateUnitMeasurementCommand>
{
    public CreateUnitMeasurementCommandValidation()
    {
        RuleFor(material => material.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}