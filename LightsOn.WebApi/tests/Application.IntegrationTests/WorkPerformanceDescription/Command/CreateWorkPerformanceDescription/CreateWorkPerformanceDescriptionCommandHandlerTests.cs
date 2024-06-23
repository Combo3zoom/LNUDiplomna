using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.WorkPerformanceDescription.Command.CreateWorkPerformanceDescription;

[Collection("Tests")]
public partial class CreateWorkPerformanceDescriptionCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public CreateWorkPerformanceDescriptionCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomWorkPerformanceDescriptionTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomWorkPerformanceDescription() }
    };

    private static Domain.Entities.WorkPerformanceDescription CreateRandomWorkPerformanceDescription()
        => CreateWorkPerformanceDescriptionFilter().Create();

    private static Filler<Domain.Entities.Client> CreateClientFilter()
    {
        var filler = new Filler<Domain.Entities.Client>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
    
    private static Filler<Domain.Entities.Engine> CreateEngineFilter()
    {
        var filler = new Filler<Domain.Entities.Engine>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt();

        return filler;
    }
    
    private static Filler<Domain.Entities.PowerEquipment> CreatePowerEquipmentFilter()
    {
        var filler = new Filler<Domain.Entities.PowerEquipment>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt();

        return filler;
    }
    private static Filler<Domain.Entities.WorkPerformanceDescription> CreateWorkPerformanceDescriptionFilter()
    {
        var filler = new Filler<Domain.Entities.WorkPerformanceDescription>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Client).Use(() => CreateClientFilter().Create())
            .OnProperty(x => x.PowerEquipment)
            .Use(() => CreatePowerEquipmentFilter().Create())
            .OnProperty(x => x.Engine).Use(() => CreateEngineFilter().Create())
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
}