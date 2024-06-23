using FluentAssertions;
using LightsOn.Application.CategoryExpense.Queries.GetCategoryExpenses;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Queries.GetCategoryExpenses;

public partial class GetCategoryExpensesQueryHandlerTests
{
    [Fact]
    public async Task ShouldGetGetCategoryExpensesOnHandleAsync()
    {
        // given
        var expectedCategoryExpenses = new List<Domain.Entities.CategoryExpense>()
        {
            CreateRandomCategoryExpenses(),
            CreateRandomCategoryExpenses(),
            CreateRandomCategoryExpenses(),
        };
        
        // when
        var categoryExpensesHandle = await this._getCategoryExpensesQueryHandler
            .Handle(new GetCategoryExpensesQuery(), CancellationToken.None);
        
        // then
        for(var i=0; i<categoryExpensesHandle.Count; i++)
            categoryExpensesHandle[i].Name.Should().Be(expectedCategoryExpenses[i].Name);

        this._mockContext.VerifyNoOtherCalls();
    }
    
    // [Test]
    // [TestCase(null)]
    // public async Task ShouldGetCategoryExpensesIfCategoryExpensesEmptyOnHandleAsync
    //     (List<Domain.Entities.CategoryExpense> expectedCategoryExpenses)
    // {
    //     // given
    //
    //     // when
    //     var categoryExpensesHandle = await this._getCategoryExpensesQueryHandler
    //         .Handle(new GetCategoryExpensesQuery(), CancellationToken.None);
    //     
    //     // then
    //     categoryExpensesHandle.Should().BeNull();
    //     
    //     //this._mockContext.VerifyNoOtherCalls();
    // }
}