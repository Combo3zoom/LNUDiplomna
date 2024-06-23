using FluentAssertions;
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Domain.Exceptions;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.CreateClient;

public partial class CreateClientCommandHandlerTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnCreateIfClientIsNullAsync(string invalidClientName)
    {
        // given
        var createClientCommandValidation = new CreateClientCommandValidation();

        // when
        var validationResult =
            await createClientCommandValidation.ValidateAsync(new CreateClientCommand(invalidClientName));

        // then
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnCreateIfClientNameIsLargerAsync()
    {
        // given
        string invalidName = new string('a', 250);

        var createClientCommandValidation = new CreateClientCommandValidation();

        // when
        var validationResult =
            await createClientCommandValidation.ValidateAsync(new CreateClientCommand(invalidName));

        // then
        validationResult.IsValid.Should().BeFalse();
    }
}