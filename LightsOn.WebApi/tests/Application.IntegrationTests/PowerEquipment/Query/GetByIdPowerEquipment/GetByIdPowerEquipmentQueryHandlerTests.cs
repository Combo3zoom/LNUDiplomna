using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Query.GetByIdPowerEquipment;

[Collection("Tests")]
public partial class GetByIdPowerEquipmentQueryHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public GetByIdPowerEquipmentQueryHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomPowerEquipmentTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomPowerEquipment() }
    };

    private static Domain.Entities.PowerEquipment CreateRandomPowerEquipment() => CreatePowerEquipmentFilter().Create();

    private static Filler<Domain.Entities.PowerEquipment> CreatePowerEquipmentFilter()
    {
        var filler = new Filler<Domain.Entities.PowerEquipment>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt();

        return filler;
    }
}