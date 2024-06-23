using FluentAssertions;
using LightsOn.Application.Client.Commands.CreateClient;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Command.CreateClient;

public partial class CreateClientCommandHandlerTests
{
    [Fact]
    public async Task ShouldCreateClientAndReturnClientIdOnHandleAsync()
    {
        // given
        Domain.Entities.Client inputClient = CreateRandomClient();
        const int expectedClientId = 0;

        _createClientCommandHandlerStorageBroker
            .Setup(broker => broker.CreateClient(It.IsAny<CreateClientCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedClientId);
        
        // when
        var actualStudentId = await this._handler.Handle(new CreateClientCommand(inputClient.Name),
            CancellationToken.None);

        //then
        actualStudentId.Should().Be(expectedClientId);

        this._mockContext.VerifyNoOtherCalls();
    }
    [Fact]
    public async Task ShouldCreateTreeClientAndCheckClientInMockContextOnHandleAsync()
    {
        // given
        Domain.Entities.Client inputFirstClient = CreateRandomClient();
        Domain.Entities.Client inputSecondClient = CreateRandomClient();
        Domain.Entities.Client inputThirdClient = CreateRandomClient();
        const int expectedClientId = 2;
        
        _createClientCommandHandlerStorageBroker
            .Setup(broker => broker.CreateClient(It.IsAny<CreateClientCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedClientId);

        // when
        await this._handler.Handle(new CreateClientCommand(inputFirstClient.Name),
            CancellationToken.None);
        await this._handler.Handle(new CreateClientCommand(inputSecondClient.Name),
            CancellationToken.None);
        var actualStudentId = await this._handler.Handle(new CreateClientCommand(inputThirdClient.Name),
            CancellationToken.None);
        
        //then
        actualStudentId.Should().Be(expectedClientId);
        this._mockContext.VerifyNoOtherCalls();
    }

    class ClientNameEqualityComparer : IEqualityComparer<Domain.Entities.Client>
    {
        public bool Equals(Domain.Entities.Client? x, Domain.Entities.Client? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Name == y.Name;
        }

        public int GetHashCode(Domain.Entities.Client obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}