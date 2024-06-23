using LightsOn.Application.Engine.Commands.CreateEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Query.GetByIdEngine;

public partial class GetByIdEngineQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldGetByIdClient(Domain.Entities.Engine randomEngine)
    {
        var createEngineCommand = new CreateEngineCommand(randomEngine.Name, randomEngine.SerialNumber);
        var engineId = await _testing.SendAsync(createEngineCommand);
        var engine = await _testing.FindAsync<Domain.Entities.Engine>(engineId);
        var getByIdEngineQuery = new Application.Engine.Queries.GetByIdEngine.GetByIdEngine(engineId);
        var gotEngine = await _testing.SendAsync(getByIdEngineQuery);

        engine.Should().NotBeNull();
        engine!.Name.Should().Be(gotEngine.Name);
    }
}