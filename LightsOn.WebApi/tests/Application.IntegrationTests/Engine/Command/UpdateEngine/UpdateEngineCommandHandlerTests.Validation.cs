
using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Engine.Commands.CreateEngine;
using LightsOn.Application.Engine.Commands.UpdateEngine;

namespace LightsOn.Application.IntegrationTests.Engine.Command.UpdateEngine;

public partial class UpdateEngineCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldRequireValidEngineId(Domain.Entities.Engine randomClient)
    {
        var nonExistEngine = new UpdateEngineCommand(randomClient.Id, randomClient.Name, "serialNumber_2");
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistEngine)).Should().ThrowAsync<Exception>();
    }

    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateIfEngineNameIsLargerException(Domain.Entities.Engine randomEngine)
    {
        var createdEngineCommand = new CreateEngineCommand(randomEngine.Name, randomEngine.SerialNumber);
        var engineId = await _testing.SendAsync(createdEngineCommand);

        var updatedEngineCommand = new UpdateEngineCommand(engineId, new string('a', 250), randomEngine.SerialNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(updatedEngineCommand)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData("name1", "serialNumber", "name2", "serialNumber_2")]
    public async Task ShouldThrowExceptionOnUpdateIfEngineSerialNumberIsNonEqual(string firstEngineName,
        string serialNumber, string secondEngineName, string serialNumber2)
    {
        var firstCommand = new CreateEngineCommand(firstEngineName, serialNumber);
        var secondCommand = new CreateEngineCommand(secondEngineName, serialNumber2);
        
        await _testing.SendAsync(firstCommand);
        var engineId = await _testing.SendAsync(secondCommand);
        
        var thirdCommand = new UpdateEngineCommand(engineId, secondEngineName, serialNumber);
 
        await FluentActions.Invoking(() =>
            _testing.SendAsync(thirdCommand)).Should().ThrowAsync<Exception>();
    }
}