using LightsOn.Application.Estimate.Commands.CreateEstimate;

namespace LightsOn.Application.IntegrationTests.Estimate.Query.GetEstimates;

public partial class GetEstimatesQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(RandomEstimateTestCaseSource))]
    public async Task ShouldGetEstimates(Domain.Entities.Estimate exceptedFirstEstimate,
        Domain.Entities.Estimate exceptedSecondEstimate)
    {
        var firstCategoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedFirstEstimate.CategoryExpense.Name);
        var firstMaterial = new CreateMaterialCommandForEstimateCommand(exceptedFirstEstimate.Material.FullName,
            exceptedFirstEstimate.Material.ShortName, exceptedFirstEstimate.Material.Model!,
            exceptedFirstEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedFirstEstimate.Material.UnitMeasurement.Name));
        
        var createFirstEstimateCommand = new CreateEstimateCommand(firstCategoryExpense, firstMaterial, 
            exceptedFirstEstimate.MaterialsCount, exceptedFirstEstimate.UsedMaterialsCount);
        
        var secondCategoryExpense = new CreateCategoryExpenseForEstimateCommand(exceptedSecondEstimate.CategoryExpense.Name);
        var secondMaterial = new CreateMaterialCommandForEstimateCommand(exceptedSecondEstimate.Material.FullName,
            exceptedSecondEstimate.Material.ShortName, exceptedSecondEstimate.Material.Model!, exceptedSecondEstimate.Material.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedSecondEstimate.Material.UnitMeasurement.Name));
        
        var createSecondEstimateCommand = new CreateEstimateCommand(secondCategoryExpense, secondMaterial, 
            exceptedSecondEstimate.MaterialsCount, exceptedSecondEstimate.UsedMaterialsCount);
        
        await _testing.SendAsync(createFirstEstimateCommand);
        await _testing.SendAsync(createSecondEstimateCommand);
        
        var getByIdClientQuery = new Application.Estimate.Queries.GetEstimates.GetEstimates();
        var actualClient = await _testing.SendAsync(getByIdClientQuery);

        actualClient.Should().NotBeNull();
        actualClient.Count.Should().Be(2);
    }
}