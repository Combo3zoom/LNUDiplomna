using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Queries.GetByIdCustomer;
using LightsOn.Application.Customer.Queries.GetCustomers;
using LightsOn.WebApi.Infrastructure;

namespace LightsOn.WebApi.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCustomers)
            .MapGet(GetByIdCustomer, "{id}")
            .MapPost(CreateCustomer);
    }

    public async Task<List<CustomerBriefDto>> GetCustomers(ISender sender)
    {
        return await sender.Send(new GetCustomersQuery());
    }
    
    public async Task<CustomerBriefDto> GetByIdCustomer(ISender sender, int id)
    {
        return await sender.Send(new GetByIdCustomer(id));
    }

    public async Task<int> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        return await sender.Send(command);
    }
}