using System.Text.RegularExpressions;

namespace LightsOn.Application.CompanyPhoneNumber.Commands.CreateCompanyPhoneNumber;

public class CreateCompanyPhoneNumberCommandValidation : AbstractValidator<CreateCompanyPhoneNumberCommand>
{
    public CreateCompanyPhoneNumberCommandValidation()
    {
        var phoneRegex = new Regex(@"^(\+?380|0)?(\s|-)?\d{2}(\s|-)?\d{3}(\s|-)?\d{2}(\s|-)?\d{2}$");
        
        RuleFor(cpn => cpn.PhoneNumber)
            .NotEmpty();
    }
}