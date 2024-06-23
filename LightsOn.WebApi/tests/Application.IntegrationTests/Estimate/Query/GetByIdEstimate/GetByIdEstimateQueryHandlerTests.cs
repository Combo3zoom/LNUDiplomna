using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Estimate.Query.GetByIdEstimate;

[Collection("Tests")]
public partial class GetByIdEstimateQueryHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;

    public GetByIdEstimateQueryHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> RandomEstimateTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomEstimate() }
    };

    private static Domain.Entities.Estimate CreateRandomEstimate() =>
        CreateEstimateFilter().Create();

    private static Filler<Domain.Entities.UnitMeasurement> CreateUnitMeasurementFilter()
    {
        var filler = new Filler<Domain.Entities.UnitMeasurement>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Materials).IgnoreIt()
            .OnProperty(x => x.MaterialsId).IgnoreIt();

        return filler;
    }

    private static Filler<Domain.Entities.CategoryExpense> CreateCategoryExpenseFilter()
    {
        var filler = new Filler<Domain.Entities.CategoryExpense>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.EstimateId).IgnoreIt()
            .OnProperty(x => x.Created).Use( () => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }

    private static Filler<Domain.Entities.Material> CreateMaterialFilter()
    {
        var filler = new Filler<Domain.Entities.Material>();
        var random = new Random();

        var nameGenerator = new RandomListItem<string>(new[] { "Material1", "Material2", "Material3" });
        var shortNameGenerator = new RandomListItem<string>(new[] { "M1", "M2", "M3" });
        var modelGenerator = new RandomListItem<string?>(new[] { "ModelA", "ModelB", null });
        var costGenerator = Convert.ToDecimal(random.NextDouble() * 1000);

        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.FullName).Use(nameGenerator)
            .OnProperty(x => x.ShortName).Use(shortNameGenerator)
            .OnProperty(x => x.Model).Use(modelGenerator)
            .OnProperty(x => x.Cost).Use(costGenerator)
            .OnProperty(x => x.UnitMeasurementId).IgnoreIt()
            .OnProperty(x => x.UnitMeasurement).Use(() => CreateUnitMeasurementFilter().Create())
            .OnProperty(x => x.EstimateId).IgnoreIt()
            .OnProperty(x => x.Estimates).IgnoreIt();
        
        return filler;
    }
    
    private static Filler<Domain.Entities.Estimate> CreateEstimateFilter()
    {
        var estimateFiller = new Filler<Domain.Entities.Estimate>();
        var random = new Random();
        uint materialsCount = (uint)random.Next(100);
        var usedMaterialCount = (uint)random.Next((int)(materialsCount+1));

        estimateFiller.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null)
            .OnProperty(x => x.MaterialsCount).Use(() => materialsCount)
            .OnProperty(x => x.UsedMaterialsCount).Use(() => usedMaterialCount)
            .OnProperty(x => x.MaterialId).IgnoreIt()
            .OnProperty(x => x.CategoryExpense).Use(() => CreateCategoryExpenseFilter().Create())
            .OnProperty(x => x.MaterialId).IgnoreIt()
            .OnProperty(x => x.Material).Use(() => CreateMaterialFilter().Create()); 

        return estimateFiller;
    }
}