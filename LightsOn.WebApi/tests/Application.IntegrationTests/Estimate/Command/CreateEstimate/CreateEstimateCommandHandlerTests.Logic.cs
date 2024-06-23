using LightsOn.Application.Estimate.Commands.CreateEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.CreateEstimate;

public partial class CreateEstimateCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEstimateTestCaseSource))]
    public async Task ShouldCreateEstimate(Domain.Entities.Estimate exceptedEstimate) 
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
        var actualEstimate = await _testing.GetEstimateWithIncludesAsync<Domain.Entities.Estimate>(estimateId);
        
        actualEstimate!.MaterialsCount.Should().Be(exceptedEstimate.MaterialsCount);
        actualEstimate!.UsedMaterialsCount.Should().Be(exceptedEstimate.UsedMaterialsCount);
        actualEstimate!.CreatedBy.Should().Be(userId);
        actualEstimate.Created.Should().BeExactly(time);
        actualEstimate.LastModifiedBy.Should().Be(userId);
        actualEstimate.LastModified.Should().BeExactly(time);
        
        actualEstimate.CategoryExpense.Name.Should().Be(exceptedEstimate.CategoryExpense.Name);
        actualEstimate.Material.FullName.Should().Be(exceptedEstimate.Material.FullName);
        actualEstimate.Material.ShortName.Should().Be(exceptedEstimate.Material.ShortName);
        actualEstimate.Material.Model.Should().Be(exceptedEstimate.Material.Model);
        actualEstimate!.Material.Cost.Should().BeApproximately(exceptedEstimate.Material.Cost,
            Convert.ToDecimal(Math.Pow(10, -9)));
    }
}