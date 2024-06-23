

using LightsOn.Application.Common.Exceptions;

namespace LightsOn.Application.IntegrationTests.Client.Command.CreateClient;
using LightsOn.Application.Client.Commands.CreateClient;
using static LightsOn.Application.IntegrationTests.Testing;

public partial class CreateClientCommandHandlerTests : BaseTestFixture
{
    [Theory]
    [InlineData("")]
    public async Task ShouldThrowNullException(string clientName)
    {
        var nonExistedClient = new CreateClientCommand(clientName);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(nonExistedClient)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnCreateIfClientNameIsLargerException()
    {
        var command = new CreateClientCommand(new string('a', 250));
 
        await FluentActions.Invoking(() => _testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();

    }
}