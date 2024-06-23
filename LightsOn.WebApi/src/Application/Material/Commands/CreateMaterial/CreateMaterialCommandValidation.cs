namespace LightsOn.Application.Material.Commands.CreateMaterial;

public class CreateMaterialCommandValidation : AbstractValidator<CreateMaterialCommand>
{
    public CreateMaterialCommandValidation()
    {
        RuleFor(material => material.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(material => material.ShortName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(material => material.Cost)
            .GreaterThanOrEqualTo(0);

        RuleFor(material => material.Model)
            .MaximumLength(200);

        RuleFor(material => material.UnitMeasurementForCreateMaterialCommand)
            .NotEmpty();
    }
}