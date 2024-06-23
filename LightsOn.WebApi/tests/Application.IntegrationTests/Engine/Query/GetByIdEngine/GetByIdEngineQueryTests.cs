using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.Engine.Query.GetByIdEngine;

[Collection("Tests")]
public partial class GetByIdEngineQueryTests : IClassFixture<Testing>
{
    private readonly Testing _testing;

    public GetByIdEngineQueryTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomEngineTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomEngine() }
    };

    private static Domain.Entities.Engine CreateRandomEngine() =>
        CreateEngineFilter().Create();

    private static Filler<Domain.Entities.Engine> CreateEngineFilter()
    {
        var filler = new Filler<Domain.Entities.Engine>();

        return filler;
    }
}