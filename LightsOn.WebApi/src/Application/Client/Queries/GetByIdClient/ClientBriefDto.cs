namespace LightsOn.Application.Client.Queries.GetByIdClient;

public record ClientBriefDto
{
    public string Name { get; init; }

    private ClientBriefDto() => Name = string.Empty;
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.Client, ClientBriefDto>();
    }
    
}