namespace LightsOn.Application.IntegrationTests.Engine.Query.GetByIdEngine;

public partial class GetByIdEngineQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomEngineTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfEngineNull(Domain.Entities.Engine incorrectEngine)
    {
        var nonExistedEngine = new Application.Engine.Queries.GetByIdEngine.GetByIdEngine(incorrectEngine.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedEngine)).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfEngineIdNegative(int incorrectEngineId)
    {
        var nonExistedEngine = new Application.Engine.Queries.GetByIdEngine.GetByIdEngine(incorrectEngineId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedEngine)).Should()
            .ThrowAsync<InvalidOperationException>();
    }
}