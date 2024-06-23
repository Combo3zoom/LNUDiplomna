using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Query.GetByIdPowerEquipment;

public partial class GetByIdPowerEquipmentQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldGetByIdPowerEquipment(Domain.Entities.PowerEquipment exceptedPowerEquipment)
    {
        var createdPowerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(exceptedPowerEquipment.Name));

        var getByIdPowerEquipment = await _testing
            .SendAsync(new GetByIdPowerEquipmentQuery(createdPowerEquipmentId));

        var actualPowerEquipment = await _testing.FindAsync<Domain.Entities.PowerEquipment>(createdPowerEquipmentId);

        getByIdPowerEquipment.Should().NotBeNull();
        getByIdPowerEquipment.Name.Should().Be(actualPowerEquipment!.Name);
    }
}