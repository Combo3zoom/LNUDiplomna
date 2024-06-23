
namespace LightsOn.Application.CompanyPhoneNumber.Queries.GetCompanyPhoneNumbers;

public class CompanyPhoneNumberBriefDto
{
    public string PhoneNumber { get; init; }

    private CompanyPhoneNumberBriefDto() => PhoneNumber = string.Empty;
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.CompanyPhoneNumber, CompanyPhoneNumberBriefDto>();
    }
}