using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Commands.UpdateCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.UpdateCustomer;

public partial class UpdateCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateCustomerIfCustomerNameIsEmpty(
        Domain.Entities.Customer exceptedCustomer, string incorrectName)
    {
        var createdCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var createdCustomerId = await _testing.SendAsync(createdCustomer);
        
        var incorrectCustomer = new UpdateCustomerCommand(createdCustomerId, incorrectName, exceptedCustomer.PhoneNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateCustomerIfCustomerPhoneNumberIsEmpty(
        Domain.Entities.Customer exceptedCustomer, string incorrectPhoneNumber)
    {
        var createdCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var createdCustomerId = await _testing.SendAsync(createdCustomer);
        
        var incorrectCustomer = new UpdateCustomerCommand(createdCustomerId, exceptedCustomer.Name, incorrectPhoneNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateCustomerIfCustomerNameContainSpecialSymbol(
        Domain.Entities.Customer exceptedCustomer, string incorrectName)
    {
        var createdCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var createdCustomerId = await _testing.SendAsync(createdCustomer);
        
        var incorrectCustomer = new UpdateCustomerCommand(createdCustomerId, incorrectName, exceptedCustomer.PhoneNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnUpdateCustomerIfCustomerPhoneNumberIsLessThanNeed(
        Domain.Entities.Customer exceptedCustomer, string incorrectPhoneNumber)
    {
        var createdCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);
        var createdCustomerId = await _testing.SendAsync(createdCustomer);
        
        var incorrectCustomer = new UpdateCustomerCommand(createdCustomerId, exceptedCustomer.Name, incorrectPhoneNumber);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
}