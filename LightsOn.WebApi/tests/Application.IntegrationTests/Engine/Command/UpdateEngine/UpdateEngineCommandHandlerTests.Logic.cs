using LightsOn.Application.Engine.Commands.CreateEngine;
using LightsOn.Application.Engine.Commands.UpdateEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Command.UpdateEngine;

public partial class UpdateEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldUpdateEngineName(Domain.Entities.Engine randomEngine)
    {
        var createdEngineCommand = new CreateEngineCommand(randomEngine.Name, $"{randomEngine.SerialNumber}{Guid.NewGuid()}");
        var engineId = await _testing.SendAsync(createdEngineCommand);

        var updatedEngineCommand = new UpdateEngineCommand(engineId, "updatedNameClientTest", "serial_2");

        await _testing.SendAsync(updatedEngineCommand);

        var resultedEngine = await _testing.FindAsync<Domain.Entities.Engine>(engineId);

        resultedEngine.Should().NotBeNull();
        resultedEngine!.Name.Should().Be(updatedEngineCommand.Name);
    }
}