using Ardalis.GuardClauses;
using FluentAssertions;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.DeleteCategoryExpense;

public partial class DeleteCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnDeleteClientIfClientIdNotExist(
        Domain.Entities.CategoryExpense randomClient)
    {
        // given
        var nonExistentClientId = randomClient.Id + 1;

        // when
        Func<Task> sut = async () =>
            await _deleteCategoryExpenseHandlerCommand.Handle(
                new Application.CategoryExpense.Commands.DeleteCategoryExpense.DeleteCategoryExpenseCommand(nonExistentClientId),
                CancellationToken.None);

        // then
        await sut.Should().ThrowAsync<NotFoundException>();

        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnDeleteClientTwice(Domain.Entities.CategoryExpense inputFirstClient)
    {
        // given
        
        // when
        await _deleteCategoryExpenseHandlerCommand.Handle(new Application.CategoryExpense.Commands.DeleteCategoryExpense.DeleteCategoryExpenseCommand(inputFirstClient.Id),
                CancellationToken.None);
        
        _mockContext.Setup(context => context.CategoryExpenses.FindAsync(It.Is<object?[]?>(
                objects => objects != null && objects.Cast<int>()
                    .Any(o => o == inputFirstClient.Id)), CancellationToken.None))
            .ReturnsAsync((Domain.Entities.CategoryExpense?)null);
        
        Func<Task> secondDeleteAction = async () =>
            await _deleteCategoryExpenseHandlerCommand.Handle(new Application.CategoryExpense.Commands.DeleteCategoryExpense.DeleteCategoryExpenseCommand(inputFirstClient.Id),
                CancellationToken.None);

        // then
        await secondDeleteAction.Should().ThrowAsync<NotFoundException>();
        
        _mockContext.Verify(context => context.CategoryExpenses.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == inputFirstClient.Id)), CancellationToken.None));
        
        this._mockContext.Verify(context => context.CategoryExpenses.Remove(inputFirstClient));
        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

        _mockContext.Verify(context => context.CategoryExpenses.FindAsync(It.Is<object?[]?>(
            objects => objects != null && objects.Cast<int>()
                .Any(o => o == inputFirstClient.Id)), CancellationToken.None));
        
        this._mockContext.VerifyNoOtherCalls();
    }
}