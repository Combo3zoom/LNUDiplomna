using LightsOn.Application.Engine.Commands.DeleteEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Command.DeleteEngine;

public partial class DeleteEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldRequireValidEngineId(Domain.Entities.Engine randomCategoryExpense)
    {
        var nonExistedEngineCommand= new DeleteEngineCommand(randomCategoryExpense.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedEngineCommand)).Should().ThrowAsync<NotFoundException>();
    }
}