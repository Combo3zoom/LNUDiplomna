using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.PowerEquipment.Command.CreatePowerEquipment;

[Collection("Tests")]
public partial class CreatePowerEquipmentCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public CreatePowerEquipmentCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomPowerEquipmentTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomPowerEquipment() }
    };
    public static IEnumerable<object[]> s_randomPowerEquipmentAndLargerStringTestCaseSource = new List<object[]>
    {
        new object[] {  new string('a', 250) }
    };
    public static IEnumerable<object[]> s_randomPowerEquipmentAndEmptyStringTestCaseSource = new List<object[]>
    {
        new object[] { "" }
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