using LightsOn.Infrastructure.Identity;
using LightsOn.WebApi.Infrastructure;

namespace LightsOn.WebApi.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
