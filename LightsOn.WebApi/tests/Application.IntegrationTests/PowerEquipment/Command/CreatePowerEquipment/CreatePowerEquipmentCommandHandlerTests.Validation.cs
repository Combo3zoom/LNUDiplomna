using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.CreatePowerEquipment;

public partial class CreatePowerEquipmentCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfPowerEquipmentNameIsEmpty(string incorrectName)
    {
        var incorrectPowerEquipment = new CreatePowerEquipmentCommand(incorrectName);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectPowerEquipment)).Should().ThrowAsync<ValidationException>();
    }
 
    [Theory]
    [MemberData(nameof(s_randomPowerEquipmentAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfPowerEquipmentNameIsLargerException(string incorrectName)
    {
        var incorrectPowerEquipment = new CreatePowerEquipmentCommand(incorrectName);
 
        await FluentActions.Invoking(() => _testing.SendAsync(incorrectPowerEquipment))
            .Should().ThrowAsync<ValidationException>();

    }
}