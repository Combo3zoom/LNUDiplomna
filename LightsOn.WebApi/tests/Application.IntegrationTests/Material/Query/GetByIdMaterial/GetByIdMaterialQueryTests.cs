using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Material.Query.GetByIdMaterial;

[Collection("Tests")]
public partial class GetByIdMaterialQueryTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public GetByIdMaterialQueryTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomMaterialTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterial() }
    };

    private static Domain.Entities.Material CreateRandomMaterial() => CreateMaterialFilter().Create();
    
    private static Filler<Domain.Entities.UnitMeasurement> CreateUnitMeasurementFilter()
    {
        var filler = new Filler<Domain.Entities.UnitMeasurement>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Materials).IgnoreIt()
            .OnProperty(x => x.MaterialsId).IgnoreIt();

        return filler;
    }

    private static Filler<Domain.Entities.Material> CreateMaterialFilter()
    {
        var filler = new Filler<Domain.Entities.Material>();

        var stringGenerator = new RandomListItem<string>(new[] { "Material1", "Material2", "Material3" });
        var shortStringGenerator = new RandomListItem<string>(new[] { "M1", "M2", "M3" });
        var modelGenerator = new RandomListItem<string?>(new[] { "ModelA", "ModelB", null }); 
        
        var random = new Random();
        var costGenerator = Convert.ToDecimal(random.NextDouble() * 1000);

        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.FullName).Use(stringGenerator)
            .OnProperty(x => x.ShortName).Use(shortStringGenerator)
            .OnProperty(x => x.Model).Use(modelGenerator)
            .OnProperty(x => x.Cost).Use(costGenerator)
            .OnProperty(x => x.UnitMeasurement).Use(() => CreateUnitMeasurementFilter().Create())
            .OnProperty(x => x.UnitMeasurementId).IgnoreIt()
            .OnProperty(x => x.EstimateId).IgnoreIt()
            .OnProperty(x => x.Estimates).IgnoreIt();

        return filler;
    }
}