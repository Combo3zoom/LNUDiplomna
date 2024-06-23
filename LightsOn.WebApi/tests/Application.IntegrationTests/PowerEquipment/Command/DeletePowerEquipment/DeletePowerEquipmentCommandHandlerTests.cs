using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.DeletePowerEquipment;

[Collection("Tests")]
public partial class DeletePowerEquipmentCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public DeletePowerEquipmentCommandHandlerTests(Testing testing)
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