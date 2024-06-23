using LightsOn.Application.CategoryExpense.Commands.DeleteCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.DeleteCategoryExpense;
using static Testing;

public partial class DeleteCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldRequireValidCategoryExpenseId(Domain.Entities.CategoryExpense randomCategoryExpense)
    {
        var nonExistedCategoryExpense = new DeleteCategoryExpenseCommand(randomCategoryExpense.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedCategoryExpense)).Should().ThrowAsync<NotFoundException>();
    }
}