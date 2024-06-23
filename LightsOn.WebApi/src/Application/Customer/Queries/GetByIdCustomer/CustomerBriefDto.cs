using LightsOn.Application.Customer.Commands.CreateCustomer;

namespace LightsOn.Application.Customer.Queries.GetByIdCustomer;

public class CustomerBriefDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    private CustomerBriefDto()
    {
        Name = string.Empty;
        PhoneNumber = string.Empty;
    }

    public class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.Customer, CustomerBriefDto>();
    }
}