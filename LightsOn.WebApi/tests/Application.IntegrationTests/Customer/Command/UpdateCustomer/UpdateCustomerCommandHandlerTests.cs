using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Customer.Command.UpdateCustomer;

[Collection("Tests")]
public partial class UpdateCustomerCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;
    public UpdateCustomerCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomCustomerTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomCustomer(), "Julian", "067 345 67 89" }
    };
    
    public static IEnumerable<object[]> s_randomCustomerAndEmptyStringTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomCustomer(), ""}
    };
    public static IEnumerable<object[]> s_randomCustomerTestCaseSourceAndIncorrectName = new List<object[]>
    {
        new object[] { CreateRandomCustomer(), "Julian@2" }
    };
    public static IEnumerable<object[]> s_randomCustomerTestCaseSourceAndIncorrectPhoneNumber = new List<object[]>
    {
        new object[] { CreateRandomCustomer(), "0323124" }
    };

    private static Domain.Entities.Customer CreateRandomCustomer() => CreateCustomerFiller().Create();

    private static Filler<Domain.Entities.Customer> CreateCustomerFiller()
    {
        var filler = new Filler<Domain.Entities.Customer>();
        var generatedName =
            new RandomListItem<string>(["Олександр", "Iryna", "Максим", "Anna", "Тарас10"]);
        var generatedPhoneNumber =
            new RandomListItem<string>(["+380 12 345 67 89", "098 765 43 21", "+380 50 123 45 67"]);
        
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Name).Use(generatedName)
            .OnProperty(x => x.PhoneNumber).Use(generatedPhoneNumber)
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
}