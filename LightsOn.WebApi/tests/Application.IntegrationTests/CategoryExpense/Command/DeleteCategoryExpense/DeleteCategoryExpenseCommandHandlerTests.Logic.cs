using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Commands.DeleteCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.DeleteCategoryExpense;
using static Testing;

public partial class DeleteCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldDeleteClient(Domain.Entities.CategoryExpense randomCategoryExpense)
    {
        var categoryExpenseiD = 
            await _testing.SendAsync(new CreateCategoryExpenseCommand(randomCategoryExpense.Name));

        await _testing.SendAsync(new DeleteCategoryExpenseCommand(categoryExpenseiD));

        var deletedCategoryExpense = await _testing.FindAsync<Domain.Entities.CategoryExpense>(categoryExpenseiD);

        deletedCategoryExpense.Should().BeNull();
    }
}