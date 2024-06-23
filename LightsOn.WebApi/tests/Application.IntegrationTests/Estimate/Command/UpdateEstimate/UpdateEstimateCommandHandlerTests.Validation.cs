using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Estimate.Commands.CreateEstimate;
using LightsOn.Application.Estimate.Commands.UpdateEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.UpdateEstimate;

public partial class UpdateEstimateCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomEstimateUsedMaterialCountBiggerThanUsedMaterials))]
    public async Task ShouldThrowValidationExceptionOnCreateIfEstimateUsedMaterialCountBiggerThanUsedMaterials(
        Domain.Entities.Estimate exceptedEstimate, uint materialsCount, uint usedMaterialsCount)
    {
        var categoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedEstimate.CategoryExpense.Name);
        var material = new CreateMaterialCommandForEstimateCommand(exceptedEstimate.Material.FullName,
            exceptedEstimate.Material.ShortName, exceptedEstimate.Material.Model!, exceptedEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedEstimate.Material.UnitMeasurement.Name));
        
        var createdEstimate = new CreateEstimateCommand(categoryExpense, material, 
            exceptedEstimate.MaterialsCount, exceptedEstimate.UsedMaterialsCount);
        
        var estimateId = await _testing.SendAsync(createdEstimate);
        
        var updatedEstimate = new UpdateEstimateCommand(estimateId, materialsCount, usedMaterialsCount);

        await FluentActions.Invoking(() => _testing.SendAsync(updatedEstimate))
            .Should().ThrowAsync<ValidationException>();
    }
}