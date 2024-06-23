using LightsOn.Application.Client.Commands.CreateClient;

namespace LightsOn.Application.IntegrationTests.Client.Query.GetByIdClient;
using static LightsOn.Application.IntegrationTests.Testing;

public partial class GetByIdClientQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldGetByIdClient(Domain.Entities.Client randomClient)
    {
        var createClientCommand = new CreateClientCommand(randomClient.Name);
        var clientId = await _testing.SendAsync(createClientCommand);
        
        var client = await _testing.FindAsync<Domain.Entities.Client>(clientId);
        
        var getByIdClientQuery = new Application.Client.Queries.GetByIdClient.GetByIdClient(clientId);
        var gotClient = await _testing.SendAsync(getByIdClientQuery);

        client.Should().NotBeNull();
        client!.Name.Should().Be(gotClient.Name);
    }
}