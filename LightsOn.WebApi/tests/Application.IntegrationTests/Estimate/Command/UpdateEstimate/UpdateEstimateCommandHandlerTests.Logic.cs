using LightsOn.Application.Estimate.Commands.CreateEstimate;
using LightsOn.Application.Estimate.Commands.UpdateEstimate;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.UpdateEstimate;


public partial class UpdateEstimateCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEstimateTestCaseSource))]
    public async Task ShouldUpdateEstimateUsedMaterials(Domain.Entities.Estimate exceptedEstimate)
    {
        var time = _testing._mockDataTimeOffset.Object.Now;
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var categoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedEstimate.CategoryExpense.Name);
        var material = new CreateMaterialCommandForEstimateCommand(exceptedEstimate.Material.FullName,
            exceptedEstimate.Material.ShortName, exceptedEstimate.Material.Model!, exceptedEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedEstimate.Material.UnitMeasurement.Name));
        
        var exceptedCreateEstimateCommand = new CreateEstimateCommand(categoryExpense, material, 
            exceptedEstimate.MaterialsCount, exceptedEstimate.UsedMaterialsCount);
        
        var createdEstimateId = await _testing.SendAsync(exceptedCreateEstimateCommand);

        await _testing.SendAsync(new UpdateEstimateCommand(createdEstimateId, exceptedEstimate.MaterialsCount, 
            exceptedEstimate.UsedMaterialsCount));
        
        var actualEstimate = await _testing.GetEstimateWithIncludesAsync<Domain.Entities.Estimate>(createdEstimateId);
        
        actualEstimate!.MaterialsCount.Should().Be(exceptedEstimate.MaterialsCount);
        actualEstimate!.UsedMaterialsCount.Should().Be(exceptedEstimate.UsedMaterialsCount);
        actualEstimate.LastModifiedBy.Should().Be(userId);
        actualEstimate.LastModified.Should().BeExactly(time);
        
        // actualEstimate!.CategoryExpense.Id.Should().Be(exceptedEstimate.CategoryExpenseId);
        // actualEstimate!.Material.Id.Should().Be(exceptedEstimate.MaterialId);
        
        actualEstimate.CategoryExpense.Name.Should().Be(exceptedEstimate.CategoryExpense.Name);
        actualEstimate.Material.FullName.Should().Be(exceptedEstimate.Material.FullName);
        actualEstimate.Material.ShortName.Should().Be(exceptedEstimate.Material.ShortName);
        actualEstimate.Material.Model.Should().Be(exceptedEstimate.Material.Model);
        actualEstimate!.Material.Cost.Should().BeApproximately(exceptedEstimate.Material.Cost,
            Convert.ToDecimal(Math.Pow(10, -9)));
    }
}