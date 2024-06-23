using LightsOn.Application.Engine.Commands.CreateEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Command.CreateEngine;

public partial class CreateEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(RandomEngineTestCaseSource))]
    public async Task ShouldCreateCategoryExpense(Domain.Entities.Engine randomEngine)
    {
        var createEngineCommand = new CreateEngineCommand(randomEngine.Name, randomEngine.SerialNumber);
        var engineId = await _testing.SendAsync(createEngineCommand);
        var engine = await _testing.FindAsync<Domain.Entities.Engine>(engineId);
        
        engine!.Name.Should().Be(randomEngine.Name);
        engine!.SerialNumber.Should().Be(randomEngine.SerialNumber);
    }
}