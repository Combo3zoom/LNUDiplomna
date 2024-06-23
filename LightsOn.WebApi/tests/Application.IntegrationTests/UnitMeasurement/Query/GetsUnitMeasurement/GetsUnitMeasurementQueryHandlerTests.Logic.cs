using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetUnitMeasurements;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Query.GetsUnitMeasurement;

public partial class GetsUnitMeasurementQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldGetClients(Domain.Entities.UnitMeasurement firstRandomUnitMeasurement,
        Domain.Entities.UnitMeasurement secondRandomUnitMeasurement)
    {
        var createFirstUnitMeasurementCommand = new CreateUnitMeasurementCommand(firstRandomUnitMeasurement.Name);
        await _testing.SendAsync(createFirstUnitMeasurementCommand);
        
        var createSecondUnitMeasurementCommand = new CreateUnitMeasurementCommand(secondRandomUnitMeasurement.Name);
        await _testing.SendAsync(createSecondUnitMeasurementCommand);
        
        var getByIdUnitMeasurementQuery = new GetUnitMeasurementsQuery();
        var actualUnitMeasurement = await _testing.SendAsync(getByIdUnitMeasurementQuery);

        actualUnitMeasurement.Should().NotBeNull();
        actualUnitMeasurement.Count.Should().Be(2);
    }
}