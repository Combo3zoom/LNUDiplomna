using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Commands.UpdatePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.UpdatePowerEquipment;

public partial class UpdatePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldUpdatePowerEquipmentName(Domain.Entities.PowerEquipment randomPowerEquipment, string updateName)
    {
        var createdPowerEquipmentCommand = new CreatePowerEquipmentCommand(randomPowerEquipment.Name);
        var powerEquipmentId = await _testing.SendAsync(createdPowerEquipmentCommand);

        var updatedPowerEquipmentCommand = new UpdatePowerEquipmentCommand(powerEquipmentId, updateName);

        await _testing.SendAsync(updatedPowerEquipmentCommand);

        var actualPowerEquipment = await _testing.FindAsync<Domain.Entities.PowerEquipment>(powerEquipmentId);

        actualPowerEquipment.Should().NotBeNull();
        actualPowerEquipment!.Name.Should().Be(updatedPowerEquipmentCommand.Name);
    }
}