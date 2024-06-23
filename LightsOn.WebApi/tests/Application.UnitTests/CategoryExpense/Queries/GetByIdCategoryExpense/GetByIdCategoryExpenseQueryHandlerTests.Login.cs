using FluentAssertions;
using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.CategoryExpense.Queries.GetByIdCategoryExpense;


public partial class GetByIdCategoryExpenseQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldGetByIdCategoryExpense(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given
        var expectedCategoryExpenseBriefDto = _mapper.Map<CategoryExpenseBriefDto>(inputCategoryExpense);
        
        _getByIdCategoryExpenseStorageBroker
            .Setup(repo => repo.GetByIdCategoryExpense(
                It.IsAny<GetByIdCategoryExpenseQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((expectedCategoryExpenseBriefDto));

        // when
        var result = await _getByIdCategoryExpenseHandler.Handle(
            new GetByIdCategoryExpenseQuery(inputCategoryExpense.Id), CancellationToken.None);
                
        // then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedCategoryExpenseBriefDto);

        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCategoryExpenseTestCaseSource))]
    public async Task ShouldGetByIdClientAndCompareIfNameNotChangedOnHandleAsync(Domain.Entities.CategoryExpense inputCategoryExpense)
    {
        // given
        var expectedCategoryExpenseBriefDto = _mapper.Map<CategoryExpenseBriefDto>(inputCategoryExpense);
        
        _getByIdCategoryExpenseStorageBroker
            .Setup(repo => repo.GetByIdCategoryExpense(
                It.IsAny<GetByIdCategoryExpenseQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategoryExpenseBriefDto);
        
        var exceptedCategoryExpenseName = inputCategoryExpense.Name;

        // when
        var result = await _getByIdCategoryExpenseHandler.Handle(
            new GetByIdCategoryExpenseQuery(inputCategoryExpense.Id), CancellationToken.None);
        
        // then
        result.Name.Should().BeEquivalentTo(exceptedCategoryExpenseName);
        
        this._mockContext.VerifyNoOtherCalls();
    }
}