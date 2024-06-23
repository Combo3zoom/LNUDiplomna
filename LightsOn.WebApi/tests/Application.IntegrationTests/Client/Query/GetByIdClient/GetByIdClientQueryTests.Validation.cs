using FluentValidation;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using static LightsOn.Application.IntegrationTests.Testing;

namespace LightsOn.Application.IntegrationTests.Client.Query.GetByIdClient;

public partial class GetByIdClientQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfClientNull(Domain.Entities.Client incorrectClient)
    {
        var nonExistedClient = new Application.Client.Queries.GetByIdClient.GetByIdClient(incorrectClient.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedClient)).Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfClientIdNegative(int incorrectClientId)
    {
        var nonExistedClient = new Application.Client.Queries.GetByIdClient.GetByIdClient(incorrectClientId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedClient)).Should().ThrowAsync<NotFoundException>();
    }

}