namespace LightsOn.Application.WorkPerformanceDescription.Queries.GetByIdWorkPerformanceDescription;

public record WorkPerformanceDescriptionBriefDto
{
    public Domain.Entities.Client Client { get; init; }
    public Domain.Entities.PowerEquipment PowerEquipment { get; init; }
    public Domain.Entities.Engine Engine { get; init; }

    private WorkPerformanceDescriptionBriefDto()
    {
        Client = null!;
        PowerEquipment = null!;
        Engine = null!;
    }

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.WorkPerformanceDescription, WorkPerformanceDescriptionBriefDto>();
    }
}