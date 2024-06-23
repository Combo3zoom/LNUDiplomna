using LightsOn.Application.Engine.Commands.CreateEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Query.GetEngines;

public partial class GetEnginesQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldGetClients(Domain.Entities.Engine firstRandomEngine, Domain.Entities.Engine secondRandomEngine)
    {
        var createFirstEngineCommand = new CreateEngineCommand(firstRandomEngine.Name, firstRandomEngine.SerialNumber);
        await _testing.SendAsync(createFirstEngineCommand);
        var createSecondEngineCommand = new CreateEngineCommand(secondRandomEngine.Name, secondRandomEngine.SerialNumber);
        await _testing.SendAsync(createSecondEngineCommand);
        var getByIdEngineQuery = new Application.Engine.Queries.GetEngines.GetEngines();
        var gotEngine = await _testing.SendAsync(getByIdEngineQuery);

        gotEngine.Should().NotBeNull();
        gotEngine.Count.Should().Be(2);
    }
}