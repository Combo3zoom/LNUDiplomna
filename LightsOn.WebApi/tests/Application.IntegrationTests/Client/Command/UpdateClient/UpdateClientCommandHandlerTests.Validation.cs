
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.UpdateClient;
using LightsOn.Application.Common.Exceptions;


namespace LightsOn.Application.IntegrationTests.Client.Command.UpdateClient;

public partial class UpdateClientCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldRequireValidClientId(Domain.Entities.Client randomClient)
    {
        var nonExistClient = new UpdateClientCommand(randomClient.Id, randomClient.Name);
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistClient)).Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateIfClientNameIsLargerException(Domain.Entities.Client randomClient)
    {
        var createdClientCommand = new CreateClientCommand(randomClient.Name);
        var clientId = await _testing.SendAsync(createdClientCommand);

        var updatedClientCommand = new UpdateClientCommand(clientId, new string('a', 250));

        await FluentActions.Invoking(() =>
            _testing.SendAsync(updatedClientCommand)).Should().ThrowAsync<ValidationException>();
    }
}