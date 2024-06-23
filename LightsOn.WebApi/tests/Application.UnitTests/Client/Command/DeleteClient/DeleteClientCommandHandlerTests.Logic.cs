using FluentAssertions;
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.DeleteClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.DeleteClient;

public partial class DeleteServiceDescriptionCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldDeleteOneClientOnHandleAsync(Domain.Entities.Client randomClient)
    {
        // given
        var inputClient = randomClient;

        // _deleteClientCommandHandlerStorageBroker
        //     .Setup(broker => broker.DeleteClient(It.IsAny<DeleteCommandClient>(),
        //         It.IsAny<CancellationToken>()))
        //     .ReturnsAsync();

        // when
        await this._deleteCompanyPhoneNumberCommandHandler.Handle(new DeleteCommandClient(inputClient.Id),
            CancellationToken.None);

        // then

        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

        this._mockContext.VerifyNoOtherCalls();
    }
}