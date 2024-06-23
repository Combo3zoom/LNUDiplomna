using LightsOn.Application.Engine.Commands.CreateEngine;
using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.CreatePowerEquipment;

public partial class CreatePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldCreatePowerEquipment(Domain.Entities.PowerEquipment exceptionPowerEquipment)
    {
        var createdPowerEquipment = new CreatePowerEquipmentCommand(exceptionPowerEquipment.Name);

        var createdPowerEquipmentId = await _testing.SendAsync(createdPowerEquipment);

        var actualPowerEquipment = await _testing.FindAsync<Domain.Entities.PowerEquipment>(createdPowerEquipmentId);

        actualPowerEquipment!.Should().NotBeNull();
        actualPowerEquipment!.Name.Should().Be(createdPowerEquipment.Name);
    }
}