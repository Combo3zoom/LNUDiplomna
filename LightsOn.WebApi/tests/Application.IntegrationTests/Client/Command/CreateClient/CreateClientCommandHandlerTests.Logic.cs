using FluentValidation;
using LightsOn.Application.Client.Commands.CreateClient;

namespace LightsOn.Application.IntegrationTests.Client.Command.CreateClient;

public partial class CreateClientCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldCreateClient(Domain.Entities.Client randomClient)
    {
        var time = _testing._mockDataTimeOffset.Object.Now;
        var userId = await _testing.RunAsDefaultUserAsync();
        var createClientCommand = new CreateClientCommand(randomClient.Name);
        var clientId = await _testing.SendAsync(createClientCommand);
        var client = await _testing.FindAsync<Domain.Entities.Client>(clientId);
        
        client!.Name.Should().Be(randomClient.Name);
        client!.CreatedBy.Should().Be(userId);
        client.Created.Should().BeExactly(time);
        client.LastModifiedBy.Should().Be(userId);
        client.LastModified.Should().BeExactly(time);
    }
}