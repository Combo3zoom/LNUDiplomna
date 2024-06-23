using LightsOn.Application.PowerEquipment.Commands.DeletePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.DeletePowerEquipment;

public partial class DeletePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentTestCaseSource))]
    public async Task ShouldRequireValidPowerEquipmentId(Domain.Entities.PowerEquipment incorrectPowerEquipment)
    {
        var nonExistedPowerEquipment = new DeletePowerEquipmentCommand(incorrectPowerEquipment.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedPowerEquipment)).Should().ThrowAsync<NotFoundException>();
    }
}