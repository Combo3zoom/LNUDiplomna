using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Query.GetByIdPowerEquipment;

public partial class GetByIdPowerEquipmentQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfPowerEquipmentIdNonExist(
        Domain.Entities.PowerEquipment incorrectPowerEquipment)
    {
        var nonExistedPowerEquipment = new GetByIdClient(incorrectPowerEquipment.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedPowerEquipment))
            .Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfPowerEquipmentIdNegative(int incorrectPowerEquipmentId)
    {
        var nonExistedPowerEquipment = new GetByIdPowerEquipmentQuery(incorrectPowerEquipmentId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedPowerEquipment))
            .Should().ThrowAsync<InvalidOperationException>();
    }
}