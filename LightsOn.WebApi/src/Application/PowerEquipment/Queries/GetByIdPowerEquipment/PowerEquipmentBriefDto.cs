namespace LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

public record PowerEquipmentBriefDto
{
    public string Name { get; init; }

    public PowerEquipmentBriefDto() => Name = string.Empty;
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.PowerEquipment, PowerEquipmentBriefDto>();
    }
}