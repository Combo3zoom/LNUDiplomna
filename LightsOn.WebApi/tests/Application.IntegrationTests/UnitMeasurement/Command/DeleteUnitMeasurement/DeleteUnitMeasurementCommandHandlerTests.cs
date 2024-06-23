using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.DeleteUnitMeasurement;

[Collection("Tests")]
public partial class DeleteUnitMeasurementCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public DeleteUnitMeasurementCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomUnitMeasurementTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomUnitMeasurement() }
    };

    private static Domain.Entities.UnitMeasurement CreateRandomUnitMeasurement() => CreateUnitMeasurementFilter().Create();

    private static Filler<Domain.Entities.UnitMeasurement> CreateUnitMeasurementFilter()
    {
        var filler = new Filler<Domain.Entities.UnitMeasurement>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x=> x.Materials).IgnoreIt()
            .OnProperty(x => x.MaterialsId).IgnoreIt();

        return filler;
    }
}