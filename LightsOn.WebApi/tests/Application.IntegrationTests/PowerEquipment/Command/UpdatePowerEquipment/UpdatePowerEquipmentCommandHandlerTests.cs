using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.UpdatePowerEquipment;

[Collection("Tests")]
public partial class UpdatePowerEquipmentCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public UpdatePowerEquipmentCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomPowerEquipmentTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomPowerEquipment(), "newName" }
    };
    public static IEnumerable<object[]> s_randomPowerEquipmentAndLargerStringTestCaseSource = new List<object[]>
    {
        new object[] {  CreateRandomPowerEquipment(), new string('a', 250) }
    };
    public static IEnumerable<object[]> s_randomPowerEquipmentAndEmptyStringTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomPowerEquipment(), "" }
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