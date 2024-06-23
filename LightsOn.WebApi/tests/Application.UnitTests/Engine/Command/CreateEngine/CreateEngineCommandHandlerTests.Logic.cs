using FluentAssertions;
using LightsOn.Application.Engine.Commands.CreateEngine;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Engine.Command.CreateEngine;

public partial class CreateEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldCreateEngineAndReturnClientIdOnHandleAsync(Domain.Entities.Engine inputEngine)
    {
        // given
        const int expectedClientId = 0;

        // when
        var actualEngineId = await this._handler.Handle(new CreateEngineCommand(inputEngine.Name,
                inputEngine.SerialNumber), CancellationToken.None);

        //then
        actualEngineId.Should().Be(expectedClientId);

        // this._mockContext.Verify(context => context.Engine
        //     .Add(It.Is(inputEngine, new EngineNameEqualityComparer())));
        
        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Fact]
    public async Task ShouldCreateTreeEngineAndCheckEngineInMockContextOnHandleAsync()
    {
        // given
        Domain.Entities.Engine inputFirstEngine = CreateRandomEngine();
        Domain.Entities.Engine inputSecondEngine = CreateRandomEngine();
        Domain.Entities.Engine inputThirdEngine = CreateRandomEngine();


        // when
        await this._handler.Handle(new CreateEngineCommand(inputFirstEngine.Name, inputFirstEngine.SerialNumber),
            CancellationToken.None);
        
        await this._handler.Handle(new CreateEngineCommand(inputSecondEngine.Name, inputSecondEngine.SerialNumber),
            CancellationToken.None);
        
        await this._handler.Handle(new CreateEngineCommand(inputThirdEngine.Name, inputThirdEngine.SerialNumber),
            CancellationToken.None);

        //then

        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None),
            Times.Never);

        this._mockContext.VerifyNoOtherCalls();
    }

    class EngineNameEqualityComparer : IEqualityComparer<Domain.Entities.Engine>
    {
        public bool Equals(Domain.Entities.Engine? x, Domain.Entities.Engine? y)
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

        public int GetHashCode(Domain.Entities.Engine obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}