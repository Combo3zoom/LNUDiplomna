namespace LightsOn.Application.Material.Commands.UpdateMaterial;

public class UpdateMaterialCommandValidation : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateMaterialCommandValidation()
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
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(material => material.UnitMeasurementForUpdateMaterialCommand)
            .NotEmpty();
    }
}