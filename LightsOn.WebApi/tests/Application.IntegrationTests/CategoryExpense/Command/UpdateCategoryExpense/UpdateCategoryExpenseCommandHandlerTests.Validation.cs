using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;
using LightsOn.Application.Common.Exceptions;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.UpdateCategoryExpense;
using static Testing;

public partial class UpdateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldRequireValidClientId(Domain.Entities.CategoryExpense randomClient)
    {
        var nonExistCategoryExpense = new UpdateCategoryExpenseCommand(randomClient.Id, randomClient.Name);
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistCategoryExpense)).Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateIfClientNameIsLargerException(Domain.Entities.CategoryExpense randomClient)
    {
        var createdCategoryExpenseCommand = new CreateCategoryExpenseCommand(randomClient.Name);
        var categoryExpenseId = await _testing.SendAsync(createdCategoryExpenseCommand);

        var updatedCategoryExpenseCommand = new UpdateCategoryExpenseCommand(categoryExpenseId, new string('a', 250));

        await FluentActions.Invoking(() =>
            _testing.SendAsync(updatedCategoryExpenseCommand)).Should().ThrowAsync<ValidationException>();
    }
}