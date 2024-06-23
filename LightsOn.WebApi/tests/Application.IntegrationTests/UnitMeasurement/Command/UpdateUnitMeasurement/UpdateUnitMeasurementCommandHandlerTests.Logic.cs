using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Commands.UpdateUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.UpdateUnitMeasurement;

public partial class UpdateUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldUpdateUnitMeasurementName(Domain.Entities.UnitMeasurement randomUnitMeasurement,
        string updateName)
    {
        var createdUnitMeasurementCommand = new CreateUnitMeasurementCommand(randomUnitMeasurement.Name);
        var unitMeasurementId = await _testing.SendAsync(createdUnitMeasurementCommand);

        var updatedUnitMeasurementCommand 
            = new UpdateUnitMeasurementCommand(unitMeasurementId, updateName);

        await _testing.SendAsync(updatedUnitMeasurementCommand);

        var resultedUnitMeasurement = await _testing.FindAsync<Domain.Entities.UnitMeasurement>(unitMeasurementId);

        resultedUnitMeasurement.Should().NotBeNull();
        resultedUnitMeasurement!.Name.Should().Be(updatedUnitMeasurementCommand.Name);
    }
}