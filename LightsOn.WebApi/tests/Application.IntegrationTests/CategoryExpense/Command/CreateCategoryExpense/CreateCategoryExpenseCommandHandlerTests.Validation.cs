
using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.Common.Exceptions;

namespace LightsOn.Application.IntegrationTests.CategoryExpense.Command.CreateCategoryExpense;
using static Testing;

public partial class CreateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [InlineData("")]
    public async Task ShouldThrowExceptionOnNullCategoryExpenseNameNullException(string categoryExpenseName)
    {
        var nonExistedCategoryExpense = new CreateCategoryExpenseCommand(categoryExpenseName);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(nonExistedCategoryExpense)).Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task ShouldThrowValidationExceptionOnCreateIfCategoryExpenseNameIsLargerException()
    {
        var command = new CreateCategoryExpenseCommand(new string('a', 250));
 
        await FluentActions.Invoking(() =>
            _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}