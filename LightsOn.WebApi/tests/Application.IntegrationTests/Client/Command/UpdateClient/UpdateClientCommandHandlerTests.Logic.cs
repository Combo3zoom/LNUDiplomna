using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.UpdateClient;

namespace LightsOn.Application.IntegrationTests.Client.Command.UpdateClient;
using static LightsOn.Application.IntegrationTests.Testing;

public partial class UpdateClientCommandHandlerTests : BaseTestFixture
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldUpdateClientName(Domain.Entities.Client randomClient)
    {
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var createdClientCommand = new CreateClientCommand(randomClient.Name);
        var clientId = await _testing.SendAsync(createdClientCommand);

        var updatedClientCommand = new UpdateClientCommand(clientId, "updatedNameClientTest");

        await _testing.SendAsync(updatedClientCommand);

        var resultedClient = await _testing.FindAsync<Domain.Entities.Client>(clientId);

        resultedClient.Should().NotBeNull();
        resultedClient!.Name.Should().Be(updatedClientCommand.Name);
        resultedClient.LastModifiedBy.Should().NotBeNull();
        resultedClient.LastModifiedBy.Should().Be(userId);
        resultedClient.LastModified.Should().NotBeNull();
        resultedClient.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}