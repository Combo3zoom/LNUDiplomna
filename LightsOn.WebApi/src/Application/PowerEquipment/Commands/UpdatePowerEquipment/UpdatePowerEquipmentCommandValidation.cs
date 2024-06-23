namespace LightsOn.Application.PowerEquipment.Commands.UpdatePowerEquipment;

public class UpdatePowerEquipmentCommandValidation : AbstractValidator<UpdatePowerEquipmentCommand>
{
    public UpdatePowerEquipmentCommandValidation()
    {
        RuleFor(powerEquipment => powerEquipment.Id)
            .NotEmpty();
        
        RuleFor(powerEquipment => powerEquipment.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}