using System.Runtime.CompilerServices;
using FluentAssertions;
using LightsOn.Application.Client.Commands.UpdateClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.UpdateClient;

public partial class UpdateClientCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldUUpdateClientNameHandleAsync(Domain.Entities.Client randomClient)
    {
        // given
        // _updateClientCommandHandlerStorageBroker
        //     .Setup(broker => broker.UpdateClient(It.IsAny<UpdateClientCommand>(),
        //         It.IsAny<CancellationToken>()))
        //     .ReturnsAsync();

        const string exceptedClientName = "Rob";
        var inputClient = new UpdateClientCommand(randomClient.Id, exceptedClientName);
        
        // when
        await this._updateClientCommandHandler.Handle(inputClient, CancellationToken.None);
        
        // then
        inputClient.Name.Should().Be(exceptedClientName);

        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None),
           Times.Never);
        
        this._mockContext.VerifyNoOtherCalls();
    }
}