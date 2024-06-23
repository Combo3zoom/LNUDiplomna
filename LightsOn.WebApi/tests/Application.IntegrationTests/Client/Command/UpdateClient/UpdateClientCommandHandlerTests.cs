using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Client.Command.UpdateClient;


[Collection("Tests")]
public partial class UpdateClientCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;

    public UpdateClientCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomClientTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomClient() }
    };

    private static Domain.Entities.Client CreateRandomClient() => CreateClientFilter().Create();

    private static Filler<Domain.Entities.Client> CreateClientFilter()
    {
        var filler = new Filler<Domain.Entities.Client>();
        filler.Setup()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
}