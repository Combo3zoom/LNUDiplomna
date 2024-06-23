using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.CreateUnitMeasurement;

public partial class CreateUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfUnitMeasurementNameIsLargerException(string incorrectName)
    {
        var createdUnitMeasurement = new CreateUnitMeasurementCommand(incorrectName);
 
        await FluentActions.Invoking(() => _testing.SendAsync(createdUnitMeasurement))
            .Should().ThrowAsync<ValidationException>();
    }
}