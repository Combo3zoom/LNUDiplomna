using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Customer.Commands.CreateCustomer;

namespace LightsOn.Application.IntegrationTests.Customer.Command.CreateCustomer;

public partial class CreateCustomerCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfCustomerNameIsEmpty(
        Domain.Entities.Customer exceptedCustomer, string incorrectName)
    {
        var incorrectCustomer = new CreateCustomerCommand(
            incorrectName, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfCustomerPhoneNumberIsEmpty(
        Domain.Entities.Customer exceptedCustomer, string incorrectPhoneNumber)
    {
        var incorrectCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, incorrectPhoneNumber, exceptedCustomer.DescribeProblem);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfCustomerNameContainSpecialSymbol(
        Domain.Entities.Customer exceptedCustomer, string incorrectName)
    {
        var incorrectCustomer = new CreateCustomerCommand(
            incorrectName, exceptedCustomer.PhoneNumber, exceptedCustomer.DescribeProblem);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData(nameof(s_randomCustomerAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionOnCreateIfCustomerPhoneNumberIsLessThanNeed(
        Domain.Entities.Customer exceptedCustomer, string incorrectPhoneNumber)
    {
        var incorrectCustomer = new CreateCustomerCommand(
            exceptedCustomer.Name, incorrectPhoneNumber, exceptedCustomer.DescribeProblem);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCustomer)).Should().ThrowAsync<ValidationException>();
    }
}