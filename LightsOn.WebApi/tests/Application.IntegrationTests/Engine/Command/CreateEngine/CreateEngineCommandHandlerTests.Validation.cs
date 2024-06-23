
using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Engine.Commands.CreateEngine;

namespace  LightsOn.Application.IntegrationTests.Engine.Command.CreateEngine;

public partial class CreateEngineCommandHandlerTests
{
    [Theory]
    [InlineData("", "serialNumber")]
    public async Task ShouldThrowExceptionOnNullEngineNameNullException(string engineName, string serialNumber)
    {
        var nonExistedEngine = new CreateEngineCommand(engineName, serialNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(nonExistedEngine)).Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData("serialNumber")]
    public async Task ShouldThrowValidationExceptionOnCreateIfEngineNameIsLargerException(string serialNumber)
    {
        var command = new CreateEngineCommand(new string('a', 250), serialNumber);
 
        await FluentActions.Invoking(() =>
            _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData("name1", "serialNumber", "name2")]
    public async Task ShouldThrowExceptionOnCreateIfEngineSerialNumberIsNonEqual(string firstEngineName,
        string serialNumber, string secondEngineName)
    {
        var firstCommand = new CreateEngineCommand(firstEngineName, serialNumber);
        
        await _testing.SendAsync(firstCommand);
        
        var secondCommand = new CreateEngineCommand(secondEngineName, serialNumber);
 
        await FluentActions.Invoking(() =>
            _testing.SendAsync(secondCommand)).Should().ThrowAsync<Exception>();
    }
}