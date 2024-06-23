using LightsOn.Application.Common.Exceptions;

namespace LightsOn.Application.IntegrationTests.Customer.Query.GetByIdCustomer;

public partial class GetByIdCustomerQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfCustomerNull(Domain.Entities.Customer exceptedCustomer)
    {
        var nonExistedCustomer = new Application.Customer.Queries.GetByIdCustomer.GetByIdCustomer(exceptedCustomer.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedCustomer)).Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfCustomerIdNegative(int incorrectClientId)
    {
        var nonExistedCustomer = new Application.Customer.Queries.GetByIdCustomer.GetByIdCustomer(incorrectClientId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedCustomer)).Should().ThrowAsync<NotFoundException>();
    }
}