using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Query.GetByIdCategoryExpense;
using static Testing;

public partial class GetByIdCategoryExpenseTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldGetByIdCategoryExpense(Domain.Entities.CategoryExpense randomCategoryExpense)
    {
        var createCategoryExpenseCommand = new CreateCategoryExpenseCommand(randomCategoryExpense.Name);
        var categoryExpenseId = await _testing.SendAsync(createCategoryExpenseCommand);
        
        var categoryExpense = await _testing.FindAsync<Domain.Entities.CategoryExpense>(categoryExpenseId);
        
        var getByIdCategoryExpenseQuery = new GetByIdCategoryExpenseQuery(categoryExpenseId);
        var gotCategoryExpense = await _testing.SendAsync(getByIdCategoryExpenseQuery);

        gotCategoryExpense.Should().NotBeNull();
        gotCategoryExpense!.Name.Should().Be(categoryExpense!.Name);
    }
}