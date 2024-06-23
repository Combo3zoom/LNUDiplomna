namespace LightsOn.Application.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandValidation: AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidation()
    {
        RuleFor(n => n.Name)
            .NotEmpty()
            .Matches("^[a-zA-ZА-Яа-яЄєІіЇїҐґ'ь]+$")
                .WithMessage("Name must contain only English or Ukrainian letters.");;

        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
            .Matches(@"^(\+?380|0)?(\s|-)?\d{2}(\s|-)?\d{3}(\s|-)?\d{2}(\s|-)?\d{2}$")
                .WithMessage("Invalid Ukrainian phone number format.");;
    }
}