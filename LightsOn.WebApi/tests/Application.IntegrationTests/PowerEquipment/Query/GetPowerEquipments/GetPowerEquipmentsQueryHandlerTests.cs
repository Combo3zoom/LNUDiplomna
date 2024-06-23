using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Query.GetPowerEquipments;

[Collection("Tests")]
public partial class GetPowerEquipmentsQueryHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public GetPowerEquipmentsQueryHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomPowerEquipmentTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomPowerEquipment(), CreateRandomPowerEquipment() }
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