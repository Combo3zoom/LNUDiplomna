using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Commands.UpdateCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.UpdateCustomer;

public partial class UpdateCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldUpdateCustomerName(Domain.Entities.Customer exceptedCustomer,
        string updateName, string updatePhoneNumber)
    {
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var createdCustomerCommand = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var customerId = await _testing.SendAsync(createdCustomerCommand);

        var updatedCustomerCommand = new UpdateCustomerCommand(customerId, updateName, updatePhoneNumber);

        await _testing.SendAsync(updatedCustomerCommand);

        var resultedCustomer = await _testing.FindAsync<Domain.Entities.Customer>(customerId);

        resultedCustomer.Should().NotBeNull();
        resultedCustomer!.Name.Should().Be(updatedCustomerCommand.Name);
        resultedCustomer!.PhoneNumber.Should().Be(updatedCustomerCommand.PhoneNumber);
        resultedCustomer.LastModifiedBy.Should().NotBeNull();
        resultedCustomer.LastModifiedBy.Should().Be(userId);
        resultedCustomer.LastModified.Should().NotBeNull();
        resultedCustomer.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}