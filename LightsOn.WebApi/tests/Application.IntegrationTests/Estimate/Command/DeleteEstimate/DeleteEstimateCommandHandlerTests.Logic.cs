using LightsOn.Application.Estimate.Commands.CreateEstimate;
using LightsOn.Application.Estimate.Commands.DeleteEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.DeleteEstimate;

public partial class DeleteEstimateCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(RandomEstimateTestCaseSource))]
    public async Task ShouldDeleteEstimate(Domain.Entities.Estimate exceptedEstimate)
    {
        
        var categoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedEstimate.CategoryExpense.Name);
        var material = new CreateMaterialCommandForEstimateCommand(exceptedEstimate.Material.FullName,
            exceptedEstimate.Material.ShortName, exceptedEstimate.Material.Model!, exceptedEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedEstimate.Material.UnitMeasurement.Name));
        
        var createEstimateCommand = new CreateEstimateCommand(categoryExpense, material, 
            exceptedEstimate.MaterialsCount, exceptedEstimate.UsedMaterialsCount);

        var estimateId = await _testing.SendAsync(createEstimateCommand);
        
        await _testing.SendAsync(new DeleteEstimateCommand(estimateId));

        var deletedEstimate = await _testing.FindAsync<Domain.Entities.Estimate>(estimateId);

        deletedEstimate.Should().BeNull();
    }
}