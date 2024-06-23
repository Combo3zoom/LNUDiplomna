namespace LightsOn.Application.Material.Queries.GetByIdMaterial;

public class GetByIdMaterialQueryValidation : AbstractValidator<GetByIdMaterialQuery>
{
    public GetByIdMaterialQueryValidation()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}