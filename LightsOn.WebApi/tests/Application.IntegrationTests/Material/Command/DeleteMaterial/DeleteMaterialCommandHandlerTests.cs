using LightsOn.Application.Material.Commands.CreateMaterial;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Material.Command.DeleteMaterial;

[Collection("Tests")]
public partial class DeleteMaterialCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public DeleteMaterialCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomMaterialTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterial() }
    };
    public static IEnumerable<object[]> s_randomMaterialCommandTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterialCommand() }
    };

    private static Domain.Entities.Material CreateRandomMaterial() => CreateMaterialFilter().Create();
    private static CreateMaterialCommand CreateRandomMaterialCommand() => CreateMaterialCommandFilter().Create();
    
    private static Filler<Domain.Entities.UnitMeasurement> CreateUnitMeasurementFilter()
    {
        var filler = new Filler<Domain.Entities.UnitMeasurement>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Materials).IgnoreIt()
            .OnProperty(x => x.MaterialsId).IgnoreIt();

        return filler;
    }

    private static Filler<CreateUnitMeasurementForCreateMaterialCommand> CreateUnitMeasurementCommandFilter()
    {
        var filler = new Filler<CreateUnitMeasurementForCreateMaterialCommand>();

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
    
    private static Filler<CreateMaterialCommand> CreateMaterialCommandFilter()
    {
        var filler = new Filler<CreateMaterialCommand>();

        var stringGenerator = new RandomListItem<string>(new[] { "Material1", "Material2", "Material3" });
        var shortStringGenerator = new RandomListItem<string>(new[] { "M1", "M2", "M3" });
        var modelGenerator = new RandomListItem<string?>(new[] { "ModelA", "ModelB", null }); 
        
        var random = new Random();
        var costGenerator = Convert.ToDecimal(random.NextDouble() * 1000);

        filler.Setup()
            .OnProperty(x => x.FullName).Use(stringGenerator)
            .OnProperty(x => x.ShortName).Use(shortStringGenerator)
            .OnProperty(x => x.Model).Use(modelGenerator!)
            .OnProperty(x => x.Cost).Use(costGenerator)
            .OnProperty(x => x.UnitMeasurementForCreateMaterialCommand)
            .Use(() => CreateUnitMeasurementCommandFilter().Create());
        return filler;
    }
}