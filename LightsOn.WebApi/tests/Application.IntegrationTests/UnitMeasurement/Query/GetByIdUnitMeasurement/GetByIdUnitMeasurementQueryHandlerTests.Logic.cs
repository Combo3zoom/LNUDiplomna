using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Query.GetByIdUnitMeasurement;

public partial class GetByIdUnitMeasurementQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldGetByIdUnitMeasurement(Domain.Entities.UnitMeasurement randomUnitMeasurement)
    {
        var createUnitMeasurementCommand = new CreateUnitMeasurementCommand(randomUnitMeasurement.Name);
        var unitMeasurementId = await _testing.SendAsync(createUnitMeasurementCommand);
        
        var unitMeasurement = await _testing.FindAsync<Domain.Entities.UnitMeasurement>(unitMeasurementId);
        
        var getByIdUnitMeasurementQuery = new GetByIdUnitMeasurementQuery(unitMeasurementId);
        var actualUnitMeasurement = await _testing.SendAsync(getByIdUnitMeasurementQuery);

        unitMeasurement.Should().NotBeNull();
        unitMeasurement!.Name.Should().Be(actualUnitMeasurement.Name);
    }
}