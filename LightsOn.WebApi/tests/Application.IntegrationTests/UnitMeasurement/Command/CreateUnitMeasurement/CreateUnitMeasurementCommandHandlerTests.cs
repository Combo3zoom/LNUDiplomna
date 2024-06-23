using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Command.CreateUnitMeasurement;

[Collection("Tests")]
public partial class CreateUnitMeasurementCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public CreateUnitMeasurementCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomUnitMeasurementTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomUnitMeasurement() }
    };
    public static IEnumerable<object[]> s_randomUnitMeasurementAndLargerStringTestCaseSource = new List<object[]>
    {
        new object[] {  new string('a', 250) }
    };
    public static IEnumerable<object[]> s_randomUnitMeasurementAndEmptyStringTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomUnitMeasurement(), "" }
    };

    private static Domain.Entities.UnitMeasurement CreateRandomUnitMeasurement() => CreateUnitMeasurementFilter().Create();

    private static Filler<Domain.Entities.UnitMeasurement> CreateUnitMeasurementFilter()
    {
        var filler = new Filler<Domain.Entities.UnitMeasurement>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Materials).IgnoreIt()
            .OnProperty(x => x.MaterialsId).IgnoreIt();

        return filler;
    }
}