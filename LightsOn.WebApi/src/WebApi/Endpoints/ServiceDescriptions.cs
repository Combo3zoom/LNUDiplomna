using LightsOn.Application.ServiceDescription.Commands.CreateServiceDescription;
using LightsOn.Application.ServiceDescription.Commands.DeleteServiceDescription;
using LightsOn.Application.ServiceDescription.Queries.GetServiceDescriptions;
using LightsOn.WebApi.Infrastructure;

namespace LightsOn.WebApi.Endpoints;

public class ServiceDescriptions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetServiceDescriptions)
            .MapPost(CreateServiceDescription)
            .MapDelete(DeleteServiceDescription, "{id}");
    }
    
    public async Task<List<ServiceDescriptionBriefDto>> GetServiceDescriptions(ISender sender)
    {
        return await sender.Send(new GetServiceDescriptionsQuery());
    }

    public async Task<int> CreateServiceDescription(ISender sender, CreateServiceDescriptionCommand command)
    {
        return await sender.Send(command);
    }
    
    public async Task<IResult> DeleteServiceDescription(ISender sender, int id)
    {
        await sender.Send(new DeleteServiceDescriptionCommand(id));
        return Results.NoContent();
    }
}