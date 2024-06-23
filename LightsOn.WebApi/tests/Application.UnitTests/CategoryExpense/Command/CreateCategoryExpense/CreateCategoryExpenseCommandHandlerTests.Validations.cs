using FluentAssertions;
using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.CreateCategoryExpense;

public partial class CreateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnCreateIfCategoryExpenseIsNullAsync(string invalidClientName)
    {
        // given
        var createClientCommandValidation = new CreateCategoryExpenseCommandValidator();

        // when
        var validationResult =
            await createClientCommandValidation.ValidateAsync(
                new Application.CategoryExpense.Commands.CreateCategoryExpense.CreateCategoryExpenseCommand(invalidClientName));

        // then
        validationResult.IsValid.Should().BeFalse();
    }


    [Fact]
    public async Task ShouldThrowValidationExceptionOnCreateIfCategoryExpenseNameIsLargerAsync()
    {
        // given
        string invalidName = new string('a', 250);

        var createClientCommandValidation = new CreateCategoryExpenseCommandValidator();

        // when
        var validationResult =
            await createClientCommandValidation.ValidateAsync(new Application.CategoryExpense.Commands.CreateCategoryExpense.CreateCategoryExpenseCommand(invalidName));

        // then
        validationResult.IsValid.Should().BeFalse();
    }
}