using Tynamix.ObjectFiller;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.UpdateCategoryExpense;


[Collection("Tests")]
public partial class UpdateCategoryExpenseCommandHandlerTests : IClassFixture<Testing>
{
    private readonly Testing _testing;

    public UpdateCategoryExpenseCommandHandlerTests(Testing testing)
    {
        _testing = testing;
    }
    
    public static IEnumerable<object[]> s_randomCategoryExpenseTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomCategoryExpense() }
    };

    private static Domain.Entities.CategoryExpense CreateRandomCategoryExpense() =>
        CreateCategoryExpenseFilter().Create();

    private static Filler<Domain.Entities.CategoryExpense> CreateCategoryExpenseFilter()
    {
        var filler = new Filler<Domain.Entities.CategoryExpense>();
        filler.Setup()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
}