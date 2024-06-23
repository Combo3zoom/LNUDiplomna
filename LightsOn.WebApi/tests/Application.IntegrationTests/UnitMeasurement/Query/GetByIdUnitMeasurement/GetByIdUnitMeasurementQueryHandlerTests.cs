using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Query.GetByIdUnitMeasurement;

[Collection("Tests")]
public partial class GetByIdUnitMeasurementQueryHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public GetByIdUnitMeasurementQueryHandlerTests(Testing testing)
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