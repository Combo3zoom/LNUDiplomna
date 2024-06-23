using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Estimate.Commands.CreateEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Command.CreateEstimate;

public partial class CreateEstimateCommandHandlerTests
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
        
        var createEstimateCommand = new CreateEstimateCommand(categoryExpense, material, 
            materialsCount, usedMaterialsCount); ;
 
        await FluentActions.Invoking(() => _testing.SendAsync(createEstimateCommand))
            .Should().ThrowAsync<ValidationException>();
    }
}