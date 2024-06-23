using LightsOn.Application.CompanyPhoneNumber.Commands.CreateCompanyPhoneNumber;
using LightsOn.Application.CompanyPhoneNumber.Commands.DeleteCompanyPhoneNumber;
using LightsOn.Application.CompanyPhoneNumber.Queries.GetCompanyPhoneNumbers;
using LightsOn.Application.ServiceDescription.Queries.GetServiceDescriptions;
using LightsOn.WebApi.Infrastructure;

namespace LightsOn.WebApi.Endpoints;

public class CompanyPhoneNumbers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCompanyPhoneNumbers)
            .MapPost(CreateCompanyPhoneNumber)
            .MapDelete(DeleteCompanyPhoneNumber, "{id}");
    }
    
    public async Task<List<CompanyPhoneNumberBriefDto>> GetCompanyPhoneNumbers(ISender sender)
    {
        return await sender.Send(new GetCompanyPhoneNumbersQuery());
    }

    public async Task<int> CreateCompanyPhoneNumber(ISender sender, CreateCompanyPhoneNumberCommand command)
    {
        return await sender.Send(command);
    }
    
    public async Task<IResult> DeleteCompanyPhoneNumber(ISender sender, int id)
    {
        await sender.Send(new DeleteCompanyPhoneNumberCommand(id));
        return Results.NoContent();
    }
}