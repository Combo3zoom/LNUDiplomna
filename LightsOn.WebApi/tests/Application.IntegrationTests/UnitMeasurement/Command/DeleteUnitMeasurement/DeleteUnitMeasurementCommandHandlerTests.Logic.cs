using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Commands.DeleteUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.DeleteUnitMeasurement;

public partial class DeleteUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldDeleteUnitMeasurement(Domain.Entities.UnitMeasurement randomUnitMeasurement)
    {
        var unitMeasurementId = await _testing
            .SendAsync(new CreateUnitMeasurementCommand(randomUnitMeasurement.Name));

        await _testing.SendAsync(new DeleteUnitMeasurementCommand(unitMeasurementId));

        var deletedUnitMeasurement = await _testing.FindAsync<Domain.Entities.UnitMeasurement>(unitMeasurementId);

        deletedUnitMeasurement.Should().BeNull();
    }
}