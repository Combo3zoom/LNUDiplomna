using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Queries.GetCustomers;

namespace LightsOn.Application.IntegrationTests.Customer.Query.GetCustomers;

public partial class GetCustomersQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomersTestCaseSource))]
    public async Task ShouldGetClients(
        Domain.Entities.Customer firstRandomCustomer,
        Domain.Entities.Customer secondRandomCustomer)
    {
        var createFirstCustomerCommand = new CreateCustomerCommand(
            firstRandomCustomer.Name,
            firstRandomCustomer.PhoneNumber,
            firstRandomCustomer.DescribeProblem);
        var createSecondCustomerCommand = new CreateCustomerCommand(
            secondRandomCustomer.Name,
            secondRandomCustomer.PhoneNumber,
            firstRandomCustomer.DescribeProblem);
        
        await _testing.SendAsync(createFirstCustomerCommand);
        await _testing.SendAsync(createSecondCustomerCommand);
        
        var getByIdCustomerQuery = new GetCustomersQuery();
        var actualCustomer = await _testing.SendAsync(getByIdCustomerQuery);

        actualCustomer.Should().NotBeNull();
        actualCustomer.Count.Should().Be(2);
    }
}