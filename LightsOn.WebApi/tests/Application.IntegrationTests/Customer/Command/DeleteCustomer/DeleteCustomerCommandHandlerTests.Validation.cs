using LightsOn.Application.Customer.Commands.DeleteCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.DeleteCustomer;

public partial class DeleteCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldRequireValidClientId(Domain.Entities.Customer randomCustomer)
    {
        var nonExistedCustomer = new DeleteCustomerCommand(randomCustomer.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedCustomer)).Should().ThrowAsync<NotFoundException>();
    }
}