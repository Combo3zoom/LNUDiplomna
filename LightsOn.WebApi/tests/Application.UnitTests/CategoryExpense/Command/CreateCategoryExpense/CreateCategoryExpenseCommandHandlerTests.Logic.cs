using FluentAssertions;
using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.CreateCategoryExpense;

public partial class CreateCategoryExpenseCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldCreateCategoryExpenseAndReturnCategoryExpenseIdOnHandleAsync(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given
        const int expectedClientId = 0;

        _createCategoryExpenseCommandHandlerStorageBroker
            .Setup(broker => broker.CreateCategory(It.IsAny<CreateCategoryExpenseCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedClientId);
        
        // when
        var actualStudentId = await this._createCategoryExpenseCommandHandler
            .Handle(new Application.CategoryExpense.Commands.CreateCategoryExpense.CreateCategoryExpenseCommand(
                inputCategoryExpense.Name), CancellationToken.None);

        //then
        actualStudentId.Should().Be(expectedClientId);

        this._mockContext.VerifyNoOtherCalls();
    }

    class CategoryExpenseNameEqualityComparer : IEqualityComparer<Domain.Entities.CategoryExpense>
    {
        public bool Equals(Domain.Entities.CategoryExpense? x, Domain.Entities.CategoryExpense? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Name == y.Name;
        }

        public int GetHashCode(Domain.Entities.CategoryExpense obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}