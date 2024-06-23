using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Commands.UpdateUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.UpdateUnitMeasurement;

public partial class UpdateUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldRequireValidUnitMeasurementId(Domain.Entities.UnitMeasurement randomUnitMeasurement,
        string newFullname)
    {
        var nonExistUnitMeasurement = 
            new UpdateUnitMeasurementCommand(randomUnitMeasurement.Id, newFullname);
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistUnitMeasurement)).Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateIfUnitMeasurementNameIsLargerException(
        Domain.Entities.UnitMeasurement randomUnitMeasurement, string incorrectName)
    {
        var createdUnitMeasurementCommand = new CreateUnitMeasurementCommand(randomUnitMeasurement.Name);
        var unitMeasurementId = await _testing.SendAsync(createdUnitMeasurementCommand);

        var updatedUnitMeasurementCommand 
            = new UpdateUnitMeasurementCommand(unitMeasurementId, incorrectName);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(updatedUnitMeasurementCommand)).Should().ThrowAsync<ValidationException>();
    }
}