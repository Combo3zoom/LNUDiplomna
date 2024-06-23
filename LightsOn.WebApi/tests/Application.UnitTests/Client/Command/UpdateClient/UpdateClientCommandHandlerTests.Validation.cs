using Ardalis.GuardClauses;
using FluentAssertions;
using LightsOn.Application.Client.Commands.UpdateClient;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.UpdateClient;

public partial class UpdateClientCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateClientIfClientIdNotExistAsync(Domain.Entities.Client inputClient)
    {
        // given
        _mockContext.Setup(context => context.Clients.FindAsync(It.Is<object?[]?>(
                objects => objects != null && objects.Cast<int>()
                    .Any(o => o == inputClient.Id)), CancellationToken.None))
            .ReturnsAsync(inputClient);
        
        UpdateClientCommand invalidUpdateClient = new UpdateClientCommand(inputClient.Id + 1, inputClient.Name);

        // when
        Func<Task> invalidUpdatedClient = async () =>
            await this._updateClientCommandHandler.Handle(invalidUpdateClient, CancellationToken.None);
        
        // then
        await invalidUpdatedClient.Should().ThrowAsync<NotFoundException>();
        
        _mockContext.Verify(context => context.Clients.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == invalidUpdateClient.Id)), CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Fact]
    public async Task ShouldThrowValidationExceptionOnUpdateClientIfClientNameIsLargeAsync()
    {
        // given
        UpdateClientCommand invalidUpdateClient = new UpdateClientCommand(20, new string('a', 250));

        var updateClientCommandValidation = new UpdateClientCommandValidator();
        
        // when
        var validateResult =
            await updateClientCommandValidation.ValidateAsync(invalidUpdateClient, CancellationToken.None);

        // then
        validateResult.IsValid.Should().BeFalse();
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnUpdateClientIfClientNameIsNullAsync(string invalidClientName)
    {
        // given
        UpdateClientCommand invalidUpdateClient = new UpdateClientCommand(20, invalidClientName);
        
        var updateClientCommandValidation = new UpdateClientCommandValidator();

        // when
        var validateResult =
            await updateClientCommandValidation.ValidateAsync(invalidUpdateClient, CancellationToken.None);
        
        // then
        validateResult.IsValid.Should().BeFalse();
        
        this._mockContext.VerifyNoOtherCalls();
    }
}