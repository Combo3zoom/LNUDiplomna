using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.DeleteCategoryExpense;

public partial class DeleteCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldDeleteOneCategoryExpenseOnHandleAsync(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given

        // when
        await this._deleteCategoryExpenseHandlerCommand.Handle(
            new Application.CategoryExpense.Commands.DeleteCategoryExpense.DeleteCategoryExpenseCommand(inputCategoryExpense.Id),
            CancellationToken.None);

        // then
        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

        this._mockContext.VerifyNoOtherCalls();
    }
}