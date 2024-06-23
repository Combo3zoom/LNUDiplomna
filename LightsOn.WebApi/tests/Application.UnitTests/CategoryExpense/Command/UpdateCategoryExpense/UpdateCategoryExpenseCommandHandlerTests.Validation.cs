using Ardalis.GuardClauses;
using FluentAssertions;
using LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.UpdateCategoryExpense;

public partial class UpdateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateCategoryExpenseIfClientIdNotExistAsync(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given
        _mockContext.Setup(context => context.CategoryExpenses.FindAsync(It.Is<object?[]?>(
                objects => objects != null && objects.Cast<int>()
                    .Any(o => o == inputCategoryExpense.Id)), CancellationToken.None))
            .ReturnsAsync(inputCategoryExpense);
        
        Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand invalidUpdateCategoryExpenseCommand =
            new Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand(inputCategoryExpense.Id + 1, inputCategoryExpense.Name);

        // when
        Func<Task> invalidUpdatedCategoryExpense = async () =>
            await this._updateCategoryExpenseCommandHandler.Handle(invalidUpdateCategoryExpenseCommand, CancellationToken.None);
        
        // then
        await invalidUpdatedCategoryExpense.Should().ThrowAsync<NotFoundException>();
        
        _mockContext.Verify(context => context.CategoryExpenses.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == invalidUpdateCategoryExpenseCommand.Id)), CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Fact]
    public async Task ShouldThrowValidationExceptionOnUpdateCategoryExpenseIfCategoryExpenseNameIsLargeAsync()
    {
        // given
        Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand invalidUpdateClient =
            new Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand(20, new string('a', 250));

        var updateClientCommandValidation = new UpdateCategoryExpenseCommandValidator();
        
        // when
        var validateResult =
            await updateClientCommandValidation.ValidateAsync(invalidUpdateClient, CancellationToken.None);

        // then
        validateResult.IsValid.Should().BeFalse();
        
        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnUpdateCategoryExpenseIfCategoryExpenseNameIsNullAsync(string invalidClientName)
    {
        // given
        Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand invalidUpdateClient =
            new Application.CategoryExpense.Commands.UpdateCategoryExpense.UpdateCategoryExpenseCommand(20, invalidClientName);
        
        var updateClientCommandValidation = new UpdateCategoryExpenseCommandValidator();

        // when
        var validateResult =
            await updateClientCommandValidation.ValidateAsync(invalidUpdateClient, CancellationToken.None);
        
        // then
        validateResult.IsValid.Should().BeFalse();
        
        this._mockContext.VerifyNoOtherCalls();
    }
}