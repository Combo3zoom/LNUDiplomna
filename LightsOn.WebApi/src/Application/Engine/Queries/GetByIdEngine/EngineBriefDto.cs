namespace LightsOn.Application.Engine.Queries.GetByIdEngine;

public class EngineBriefDto
{
    public string Name { get; init; }
    public string SerialNumber { get; init; }
    
    private EngineBriefDto()
    {
        Name = string.Empty;
        SerialNumber = string.Empty;
    }

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.Engine, EngineBriefDto>();
    }
}