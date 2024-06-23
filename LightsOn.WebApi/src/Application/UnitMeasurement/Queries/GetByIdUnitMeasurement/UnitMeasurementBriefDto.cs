namespace LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

public record UnitMeasurementBriefDto
{
    public string Name { get; init; }

    private UnitMeasurementBriefDto() => Name = string.Empty;

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.UnitMeasurement, UnitMeasurementBriefDto>();
    }
}