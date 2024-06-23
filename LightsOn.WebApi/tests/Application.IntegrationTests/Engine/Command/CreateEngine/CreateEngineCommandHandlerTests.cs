using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Engine.Command.CreateEngine;
[Collection("Tests")]
public partial class CreateEngineCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;

    public CreateEngineCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> RandomEngineTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomEngine() }
    };

    private static Domain.Entities.Engine CreateRandomEngine() =>
        CreateEngineFilter().Create();

    private static Filler<Domain.Entities.Engine> CreateEngineFilter()
    {
        var filler = new Filler<Domain.Entities.Engine>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt();

        return filler;
    }
}