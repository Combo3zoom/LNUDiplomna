using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Commands.DeleteCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.DeleteCustomer;

public partial class DeleteCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldDeleteCustomer(Domain.Entities.Customer exceptedCustomer)
    {
        var customerId = await _testing.SendAsync(new CreateCustomerCommand(exceptedCustomer.Name,
            exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem));

        await _testing.SendAsync(new DeleteCustomerCommand(customerId));

        var deletedCustomer = await _testing.FindAsync<Domain.Entities.Customer>(customerId);

        deletedCustomer.Should().BeNull();
    }
}