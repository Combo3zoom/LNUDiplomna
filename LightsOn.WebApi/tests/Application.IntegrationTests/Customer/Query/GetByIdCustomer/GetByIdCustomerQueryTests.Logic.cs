using LightsOn.Application.Customer.Commands.CreateCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Query.GetByIdCustomer;

public partial class GetByIdCustomerQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldGetByIdCustomer(Domain.Entities.Customer exceptedCustomer)
    {
        var createdCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var createdCustomerId = await _testing.SendAsync(createdCustomer);
        
        var customer = await _testing.FindAsync<Domain.Entities.Customer>(createdCustomerId);
        
        var getByIdCustomerQuery = new Application.Customer.Queries.GetByIdCustomer.GetByIdCustomer(createdCustomerId);
        var actualCustomer = await _testing.SendAsync(getByIdCustomerQuery);

        actualCustomer.Should().NotBeNull();
        actualCustomer!.Name.Should().Be(customer!.Name);
        actualCustomer!.PhoneNumber.Should().Be(customer!.PhoneNumber);
    }
}