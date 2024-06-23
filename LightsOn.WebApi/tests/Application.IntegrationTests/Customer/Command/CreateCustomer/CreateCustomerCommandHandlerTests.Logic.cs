using LightsOn.Application.Customer.Commands.CreateCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.CreateCustomer;

public partial class CreateCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerTestCaseSource))]
    public async Task ShouldCreateCustomer(Domain.Entities.Customer exceptedCustomer)
    {
        var time = _testing._mockDataTimeOffset.Object.Now;
        _testing._mockTelegramBot.Setup(bot => bot.SendMessageToAllowedUsers(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Unhandled exception"));
        
        var userId = await _testing.RunAsDefaultUserAsync();
        
        var createdCustomerCommand = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        
        var customerId = await _testing.SendAsync(createdCustomerCommand);
        var actualCustomer = await _testing.FindAsync<Domain.Entities.Customer>(customerId);
        
        actualCustomer!.Name.Should().Be(exceptedCustomer.Name);
        actualCustomer.PhoneNumber.Should().Be(exceptedCustomer.PhoneNumber);
        actualCustomer.DescribeProblem.Should().Be(exceptedCustomer.DescribeProblem);
        actualCustomer.CreatedBy.Should().Be(userId);
        actualCustomer.Created.Should().BeExactly(time);
        actualCustomer.LastModifiedBy.Should().Be(userId);
        actualCustomer.LastModified.Should().BeExactly(time);
    }
}