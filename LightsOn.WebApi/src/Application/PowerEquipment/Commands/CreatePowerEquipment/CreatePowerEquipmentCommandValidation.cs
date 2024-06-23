namespace LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;

public class CreatePowerEquipmentCommandValidation : AbstractValidator<CreatePowerEquipmentCommand>
{
    public CreatePowerEquipmentCommandValidation()
    {
        RuleFor(powerEquipment => powerEquipment.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}