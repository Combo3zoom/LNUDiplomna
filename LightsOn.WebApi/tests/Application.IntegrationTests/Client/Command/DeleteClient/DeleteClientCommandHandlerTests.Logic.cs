using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.DeleteClient;

namespace LightsOn.Application.IntegrationTests.Client.Command.DeleteClient;
using static Testing;

public partial class DeleteServiceDescriptionCommandHandlerTests : BaseTestFixture
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldDeleteClient(Domain.Entities.Client randomClient)
    {
        var clientId = await _testing.SendAsync(new CreateClientCommand(randomClient.Name));

        await _testing.SendAsync(new DeleteCommandClient(clientId));

        var deletedClient = await _testing.FindAsync<Domain.Entities.Client>(clientId);

        deletedClient.Should().BeNull();
    }
}