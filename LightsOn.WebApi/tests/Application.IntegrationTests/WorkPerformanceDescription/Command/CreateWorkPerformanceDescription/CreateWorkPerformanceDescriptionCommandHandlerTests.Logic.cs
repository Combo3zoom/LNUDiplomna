using LightsOn.Application.WorkPerformanceDescription.Commands.CreateWorkPerformanceDescription;

namespace LightsOn.Application.IntegrationTests.WorkPerformanceDescription.Command.CreateWorkPerformanceDescription;

public partial class CreateWorkPerformanceDescriptionCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomWorkPerformanceDescriptionTestCaseSource))]
    public async Task ShouldCreateWorkPerformanceDescription(
        Domain.Entities.WorkPerformanceDescription exceptedWorkPerformanceDescription)
    {
        var time = _testing._mockDataTimeOffset.Object.Now;
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var createdWorkPerformanceDescriptionId = await _testing
            .SendAsync(new CreateWorkPerformanceDescriptionCommand(exceptedWorkPerformanceDescription.Client,
                exceptedWorkPerformanceDescription.PowerEquipment, exceptedWorkPerformanceDescription.Engine));

        var actualWorkPerformanceDescription = await _testing
            .FindAsync<Domain.Entities.WorkPerformanceDescription>(createdWorkPerformanceDescriptionId);

        actualWorkPerformanceDescription.Should().NotBeNull();
        actualWorkPerformanceDescription!.ClientId.Should().Be(exceptedWorkPerformanceDescription.Client.Id);
        actualWorkPerformanceDescription!.PowerEquipmentId.Should().Be(exceptedWorkPerformanceDescription.PowerEquipment.Id);
        actualWorkPerformanceDescription!.EngineId.Should().Be(exceptedWorkPerformanceDescription.Engine.Id);
        actualWorkPerformanceDescription!.CreatedBy.Should().Be(userId);
        actualWorkPerformanceDescription.Created.Should().BeExactly(time);
        actualWorkPerformanceDescription.LastModifiedBy.Should().Be(userId);
        actualWorkPerformanceDescription.LastModified.Should().BeExactly(time);
    }
}