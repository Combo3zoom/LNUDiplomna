using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Commands.UpdateMaterial;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Material.Command.UpdateMaterial;

[Collection("Tests")]
public partial class UpdateMaterialCommandHandlerTests : IClassFixture<Testing>
{
     private readonly Testing _testing;
    public UpdateMaterialCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomCreateAndUpdateMaterialTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterialCommand(), UpdateRandomMaterialCommand() }
    };
    public static IEnumerable<object[]> s_randomMaterialAndLargerStringTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterialCommand(), new string('a', 250) }
    };
    public static IEnumerable<object[]> s_randomMaterialAndEmptyStringTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterialCommand(), "" }
    };
    public static IEnumerable<object[]> s_randomMaterialAndIncorrectDecimalTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomMaterialCommand(), -10 }
    };

    private static CreateMaterialCommand CreateRandomMaterialCommand() => CreateMaterialCommandFilter().Create();
    private static UpdateMaterialCommand UpdateRandomMaterialCommand() => UpdateMaterialCommandFilter().Create();
    
    private static Filler<CreateUnitMeasurementForCreateMaterialCommand> CreateUnitMeasurementCommandFilter()
    {
        var filler = new Filler<CreateUnitMeasurementForCreateMaterialCommand>();

        return filler;
    }
    private static Filler<UpdateUnitMeasurementForUpdateMaterialCommand> UpdateUnitMeasurementCommandFilter()
    {
        var filler = new Filler<UpdateUnitMeasurementForUpdateMaterialCommand>();

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
    
    private static Filler<UpdateMaterialCommand> UpdateMaterialCommandFilter()
    {
        var filler = new Filler<UpdateMaterialCommand>();

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
            .OnProperty(x => x.UnitMeasurementForUpdateMaterialCommand)
            .Use(() => UpdateUnitMeasurementCommandFilter().Create());
        return filler;
    }
}