using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.CreateUnitMeasurement;

public partial class CreateUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldCreateUnitMeasurement(Domain.Entities.UnitMeasurement randomUnitMeasurement)
    {
        var createUnitMeasurementCommand = new CreateUnitMeasurementCommand(randomUnitMeasurement.Name);
        var unitMeasurementId = await _testing.SendAsync(createUnitMeasurementCommand);
        var unitMeasurement = await _testing.FindAsync<Domain.Entities.UnitMeasurement>(unitMeasurementId);

        unitMeasurement.Should().NotBeNull();
        unitMeasurement!.Name.Should().Be(randomUnitMeasurement.Name);
    }
}