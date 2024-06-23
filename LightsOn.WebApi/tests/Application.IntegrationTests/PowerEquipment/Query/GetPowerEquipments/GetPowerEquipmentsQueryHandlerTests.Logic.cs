using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Queries.GetPowerEquipments;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Query.GetPowerEquipments;

public partial class GetPowerEquipmentsQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldGetClients(Domain.Entities.PowerEquipment exceptedFirstPowerEquipment,
        Domain.Entities.PowerEquipment exceptedSecondPowerEquipment)
    {
        var createdFirstPowerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(exceptedFirstPowerEquipment.Name));
        
        var createdSecondPowerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(exceptedSecondPowerEquipment.Name));
        
        var actualPowerEquipment = await _testing.SendAsync(new GetPowerEquipmentsQuery());

        actualPowerEquipment.Should().NotBeNull();
        actualPowerEquipment.Count.Should().Be(2);
    }
}