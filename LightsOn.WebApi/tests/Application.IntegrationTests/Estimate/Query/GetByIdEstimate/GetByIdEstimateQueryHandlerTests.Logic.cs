using LightsOn.Application.Estimate.Commands.CreateEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Query.GetByIdEstimate;

public partial class GetByIdEstimateQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(RandomEstimateTestCaseSource))]
    public async Task ShouldGetByIdEstimate(Domain.Entities.Estimate exceptedEstimate)
    {
        var time = _testing._mockDataTimeOffset.Object.Now;
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var categoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedEstimate.CategoryExpense.Name);
        var material = new CreateMaterialCommandForEstimateCommand(exceptedEstimate.Material.FullName,
            exceptedEstimate.Material.ShortName, exceptedEstimate.Material.Model!, exceptedEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedEstimate.Material.UnitMeasurement.Name));
        
        var createEstimateCommand = new CreateEstimateCommand(categoryExpense, material, 
            exceptedEstimate.MaterialsCount, exceptedEstimate.UsedMaterialsCount);
        
        var estimateId = await _testing.SendAsync(createEstimateCommand);

        var getByIdEstimate = await _testing.SendAsync(
            new Application.Estimate.Queries.GetByIdEstimate.GetByIdEstimate(estimateId));
        var actualEstimate = await _testing.FindAsync<Domain.Entities.Estimate>(estimateId);
        
        actualEstimate!.MaterialsCount.Should().Be(getByIdEstimate.MaterialsCount);
        actualEstimate!.UsedMaterialsCount.Should().Be(getByIdEstimate.UsedMaterialsCount);
        actualEstimate.LastModifiedBy.Should().Be(userId);
        actualEstimate.LastModified.Should().BeExactly(time);
    }
}