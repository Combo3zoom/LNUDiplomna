using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Commands.DeletePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.DeletePowerEquipment;

public partial class DeletePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldDeletePowerEquipment(Domain.Entities.PowerEquipment incorrectPowerEquipment)
    {
        var powerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(incorrectPowerEquipment.Name));

        await _testing.SendAsync(new DeletePowerEquipmentCommand(powerEquipmentId));

        var deletedPowerEquipment = await _testing.FindAsync<Domain.Entities.PowerEquipment>(powerEquipmentId);

        deletedPowerEquipment.Should().BeNull();
    }
}