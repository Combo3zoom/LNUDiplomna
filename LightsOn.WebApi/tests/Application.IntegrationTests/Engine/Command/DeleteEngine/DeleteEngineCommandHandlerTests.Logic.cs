using LightsOn.Application.Engine.Commands.CreateEngine;
using LightsOn.Application.Engine.Commands.DeleteEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Command.DeleteEngine;

public partial class DeleteEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldDeleteClient(Domain.Entities.Engine randomCategoryExpense)
    {
        var engineId = 
            await _testing.SendAsync(new CreateEngineCommand(
                randomCategoryExpense.Name, randomCategoryExpense.SerialNumber));

        await _testing.SendAsync(new DeleteEngineCommand(engineId));

        var deletedEngine = await _testing.FindAsync<Domain.Entities.Engine>(engineId);

        deletedEngine.Should().BeNull();
    }
}