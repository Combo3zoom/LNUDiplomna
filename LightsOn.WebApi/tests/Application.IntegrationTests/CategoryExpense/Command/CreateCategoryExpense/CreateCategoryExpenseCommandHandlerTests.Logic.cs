using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.CreateCategoryExpense;

public partial class CreateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldCreateCategoryExpense(Domain.Entities.CategoryExpense randomCategoryExpense)
    {
        var userId = await _testing.RunAsDefaultUserAsync();
        var createCategoryExpenseCommand = new CreateCategoryExpenseCommand(randomCategoryExpense.Name);
        var categoryExpenseId = await _testing.SendAsync(createCategoryExpenseCommand);
        var categoryExpense = await _testing.FindAsync<Domain.Entities.CategoryExpense>(categoryExpenseId);
        
        categoryExpense!.Name.Should().Be(randomCategoryExpense.Name);
        categoryExpense!.CreatedBy.Should().Be(userId);
        categoryExpense.Created.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
        categoryExpense.LastModifiedBy.Should().Be(userId);
        categoryExpense.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}