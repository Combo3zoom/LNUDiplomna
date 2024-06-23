using FluentAssertions;
using FluentValidation;
using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;
namespace LightsOn.Application.UnitTests.CategoryExpense.Queries.GetByIdCategoryExpense;

public partial class GetByIdCategoryExpenseQueryHandlerTests
{
    // [Test]
    // [TestCase(null)]
    // public async Task ShouldThrowValidationExceptionOnGetByIdCategoryExpenseIfCategoryExpenseIdNull(Domain.Entities.CategoryExpense inputCategoryExpense)
    // {
    //     // given
    //     var invalidInputCategoryExpense = new GetByIdCategoryExpenseQuery(inputCategoryExpense.Id);
    //     var getByIdClientQueryValidator = new GetByIdCategoryExpenseCommandValidation();
    //
    //     // when
    //     var validateResult =
    //         await getByIdClientQueryValidator.ValidateAsync(invalidInputCategoryExpense, CancellationToken.None);
    //     
    //     // then
    //     validateResult.IsValid.Should().BeFalse();
    //     
    //     this._mockContext.VerifyNoOtherCalls();
    // }
}