using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.UpdateCategoryExpense;
using static Testing;

public partial class UpdateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldUpdateCategoryExpenseName(Domain.Entities.CategoryExpense randomClient)
    {
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var createdCategoryExpenseCommand = new CreateCategoryExpenseCommand(randomClient.Name);
        var categoryExpenseId = await _testing.SendAsync(createdCategoryExpenseCommand);

        var updatedCategoryExpenseCommand = new UpdateCategoryExpenseCommand(categoryExpenseId, "updatedNameClientTest");

        await _testing.SendAsync(updatedCategoryExpenseCommand);

        var resultedCategoryExpense = await _testing.FindAsync<Domain.Entities.CategoryExpense>(categoryExpenseId);

        resultedCategoryExpense.Should().NotBeNull();
        resultedCategoryExpense!.Name.Should().Be(updatedCategoryExpenseCommand.Name);
        resultedCategoryExpense.LastModifiedBy.Should().NotBeNull();
        resultedCategoryExpense.LastModifiedBy.Should().Be(userId);
        resultedCategoryExpense.LastModified.Should().NotBeNull();
        resultedCategoryExpense.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}