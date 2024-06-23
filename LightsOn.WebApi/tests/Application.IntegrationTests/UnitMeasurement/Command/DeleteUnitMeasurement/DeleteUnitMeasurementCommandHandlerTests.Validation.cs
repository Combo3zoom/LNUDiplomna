using LightsOn.Application.UnitMeasurement.Commands.DeleteUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.DeleteUnitMeasurement;

public partial class DeleteUnitMeasurementCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldRequireValidUnitMeasurementId(Domain.Entities.UnitMeasurement randomClient)
    {
        var nonExistedUnitMeasurement = new DeleteUnitMeasurementCommand(randomClient.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedUnitMeasurement)).Should().ThrowAsync<NotFoundException>();
    }
}