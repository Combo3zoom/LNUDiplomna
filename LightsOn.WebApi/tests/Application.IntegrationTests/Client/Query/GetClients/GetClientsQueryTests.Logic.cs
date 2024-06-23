using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Queries.GetClients;
using static LightsOn.Application.IntegrationTests.Testing;

namespace LightsOn.Application.IntegrationTests.Client.Query.GetClients;

public partial class GetClientsQueryTests : BaseTestFixture
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldGetClients(Domain.Entities.Client firstRandomClient, Domain.Entities.Client secondRandomClient)
    {
        var createFirstClientCommand = new CreateClientCommand(firstRandomClient.Name);
        await _testing.SendAsync(createFirstClientCommand);
        
        var createSecondClientCommand = new CreateClientCommand(secondRandomClient.Name);
        await _testing.SendAsync(createSecondClientCommand);
        
        var getByIdClientQuery = new GetClientsQuery();
        var actualClient = await _testing.SendAsync(getByIdClientQuery);

        actualClient.Should().NotBeNull();
        actualClient.Count.Should().Be(2);
    }
}