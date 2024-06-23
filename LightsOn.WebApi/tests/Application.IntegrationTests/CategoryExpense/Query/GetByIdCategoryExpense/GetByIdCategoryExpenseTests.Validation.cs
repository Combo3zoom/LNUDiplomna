using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Query.GetByIdCategoryExpense;
using static Testing;

public partial class GetByIdCategoryExpenseTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfCategoryExpenseNonExist(Domain.Entities.CategoryExpense incorrectCategoryExpense)
    {
        var nonExistedCategoryExpense = new Application.Client.Queries.GetByIdClient.GetByIdClient(incorrectCategoryExpense.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedCategoryExpense)).Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfCategoryExpenseIdNegative(int incorrectCategoryExpenseId)
    {
        var nonExistedCategoryExpense = new GetByIdCategoryExpenseQuery(incorrectCategoryExpenseId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedCategoryExpense)).Should().ThrowAsync<InvalidOperationException>();
    }
}