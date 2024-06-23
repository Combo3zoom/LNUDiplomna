using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Commands.UpdatePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.UpdatePowerEquipment;

public partial class UpdatePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfPowerEquipmentNameIsEmpty(
        Domain.Entities.PowerEquipment exceptedPowerEquipment, string incorrectName)
    {
        var powerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(exceptedPowerEquipment.Name));
        
        var incorrectUpdatedPowerEquipment = new UpdatePowerEquipmentCommand(powerEquipmentId, incorrectName);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedPowerEquipment)).Should().ThrowAsync<ValidationException>();
    }
 
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfPowerEquipmentNameIsLargerException(
        Domain.Entities.PowerEquipment exceptedPowerEquipment, string incorrectName)
    {
        var powerEquipmentId = await _testing
            .SendAsync(new CreatePowerEquipmentCommand(exceptedPowerEquipment.Name));
        
        var incorrectUpdatedPowerEquipment = new UpdatePowerEquipmentCommand(powerEquipmentId, incorrectName);
        
        await FluentActions.Invoking(() => _testing.SendAsync(incorrectUpdatedPowerEquipment))
            .Should().ThrowAsync<ValidationException>();

    }
}