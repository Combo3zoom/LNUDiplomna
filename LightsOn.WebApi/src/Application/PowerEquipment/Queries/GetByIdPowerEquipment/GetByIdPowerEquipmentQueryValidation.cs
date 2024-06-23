namespace LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

public class GetByIdPowerEquipmentQueryValidation : AbstractValidator<GetByIdPowerEquipmentQuery>
{
    public GetByIdPowerEquipmentQueryValidation()
    {
        RuleFor(powerEquipment => powerEquipment.Id)
            .NotEmpty();
    }
}