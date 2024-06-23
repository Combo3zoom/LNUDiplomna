namespace LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

public class GetByIdUnitMeasurementQueryValidation : AbstractValidator<GetByIdUnitMeasurementQuery>
{
    public GetByIdUnitMeasurementQueryValidation()
    {
        RuleFor(powerEquipment => powerEquipment.Id)
            .NotEmpty();
    }
}