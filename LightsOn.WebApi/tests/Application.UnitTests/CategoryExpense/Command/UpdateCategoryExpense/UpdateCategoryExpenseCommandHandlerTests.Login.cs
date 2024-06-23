using FluentAssertions;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.UpdateCategoryExpense;

public partial class UpdateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldUUpdateCategoryExpenseNameHandleAsync(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given
        const string exceptedClientName = "Rob";
        var inputClient = new Application.CategoryExpense.Commands
            .UpdateCategoryExpense.UpdateCategoryExpenseCommand(inputCategoryExpense.Id, exceptedClientName);
        
        // when
        await this._updateCategoryExpenseCommandHandler.Handle(inputClient, CancellationToken.None);
        
        // then
        inputClient.Name.Should().Be(exceptedClientName);
        
        // this._mockContext.Verify(context => context.CategoryExpense.FindAsync(It.Is<object?[]?>(
        //     objects => objects != null && objects.Cast<int>()
        //         .Any(o => o == inputClient.Id)), CancellationToken.None));
        this._mockContext.Verify(context => context.SaveChangesAsync(CancellationToken.None),
            Times.Never);
        
        this._mockContext.VerifyNoOtherCalls();
    }
}