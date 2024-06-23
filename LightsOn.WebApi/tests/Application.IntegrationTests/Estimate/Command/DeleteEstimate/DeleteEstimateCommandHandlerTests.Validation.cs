using LightsOn.Application.Estimate.Commands.DeleteEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.DeleteEstimate;

public partial class DeleteEstimateCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(RandomEstimateTestCaseSource))]
    public async Task ShouldRequireValidEstimateId(Domain.Entities.Estimate exceptedEstimate)
    {
        var nonExistedEstimate = new DeleteEstimateCommand(exceptedEstimate.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedEstimate)).Should().ThrowAsync<NotFoundException>();
    }
}