using LightsOn.Domain.Entities;

namespace LightsOn.Application.Material.Queries.GetByIdMaterial;

public record MaterialBriefDto
{
    public string FullName { get; init; }
    public string ShortName { get; init; }
    public string Model { get; init; }
    public decimal Cost { get; init; }
    public Domain.Entities.UnitMeasurement UnitMeasurement { get; init; }
    
    private MaterialBriefDto()
    {
        FullName = string.Empty;
        ShortName = string.Empty;
        Model = string.Empty;
        Cost = Decimal.Zero;
        UnitMeasurement = null!;
    }
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.Material, MaterialBriefDto>();
    }
}