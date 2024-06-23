using Ardalis.GuardClauses;
using FluentAssertions;
using LightsOn.Application.Client.Commands.DeleteClient;
using LightsOn.Domain.Exceptions;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.DeleteClient;

public partial class DeleteServiceDescriptionCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnDeleteClientIfClientIdNotExist(
        Domain.Entities.Client randomClient)
    {
        // given
        var nonExistentClientId = randomClient.Id + 1;

        // when
        Func<Task> sut = async () =>
            await _deleteCompanyPhoneNumberCommandHandler.Handle(new DeleteCommandClient(nonExistentClientId),
                CancellationToken.None);

        // then
        await sut.Should().ThrowAsync<NotFoundException>();
        
        _mockContext.Verify(context => context.Clients.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == nonExistentClientId)), CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnDeleteClientTwice(Domain.Entities.Client inputFirstClient)
    {
        // given
        _mockContext.Setup(context => context.Clients.FindAsync(It.Is<object?[]?>(
                objects => objects != null && objects.Cast<int>()
                    .Any(o => o == inputFirstClient.Id)), CancellationToken.None))
            .ReturnsAsync(inputFirstClient);
        
        // when
        await _deleteCompanyPhoneNumberCommandHandler.Handle(new DeleteCommandClient(inputFirstClient.Id),
                CancellationToken.None);
        
        _mockContext.Setup(context => context.Clients.FindAsync(It.Is<object?[]?>(
                objects => objects != null && objects.Cast<int>()
                    .Any(o => o == inputFirstClient.Id)), CancellationToken.None))
            .ReturnsAsync((Domain.Entities.Client?)null);
        
        Func<Task> secondDeleteAction = async () =>
            await _deleteCompanyPhoneNumberCommandHandler.Handle(new DeleteCommandClient(inputFirstClient.Id),
                CancellationToken.None);

        // then
        await secondDeleteAction.Should().ThrowAsync<NotFoundException>();
        
        _mockContext.Verify(context => context.Clients.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == inputFirstClient.Id)), CancellationToken.None));
        
        this._mockContext.Verify(context => context.Clients.Remove(inputFirstClient));
        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

        _mockContext.Verify(context => context.Clients.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == inputFirstClient.Id)), CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
}