namespace LightsOn.Application.ServiceDescription.Queries.GetServiceDescriptions;

public class ServiceDescriptionBriefDto
{
    public string HeaderText { get; init; } 
    public string MainText { get; init; } 
    public string LowerPriceLimit { get; init; }

    public ServiceDescriptionBriefDto()
    {
        HeaderText = string.Empty;
        MainText = string.Empty;
        LowerPriceLimit = string.Empty;
    }
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.ServiceDescription, ServiceDescriptionBriefDto>();
    }
}