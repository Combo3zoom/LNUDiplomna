using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Queries.GetCategoryExpenses;
using LightsOn.Application.Estimate.Queries.GetEstimates;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Query.GetCategoryExpenses;
using static Testing;

public partial class GetCategoryExpensesTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldGetCategoryExpenses(Domain.Entities.CategoryExpense firstRandomCategoryExpenses)
    {
        var createFirstCategoryExpensesCommand = new CreateCategoryExpenseCommand(firstRandomCategoryExpenses.Name);
        await _testing.SendAsync(createFirstCategoryExpensesCommand);
        
        var createSecondCategoryExpensesCommand = new CreateCategoryExpenseCommand(firstRandomCategoryExpenses.Name); 
        await _testing.SendAsync(createSecondCategoryExpensesCommand);
        
        var getByIdCategoryExpensesQuery = new GetCategoryExpensesQuery();
        var gotCategoryExpenses = await _testing.SendAsync(getByIdCategoryExpensesQuery);

        gotCategoryExpenses.Should().NotBeNull();
        gotCategoryExpenses.Count.Should().Be(2);
    }
}