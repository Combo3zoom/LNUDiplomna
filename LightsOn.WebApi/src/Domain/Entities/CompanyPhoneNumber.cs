namespace LightsOn.Domain.Entities;

public class CompanyPhoneNumber(string phoneNumber) : BaseEntity
{
    public string PhoneNumber { get; set; } = phoneNumber;

    private CompanyPhoneNumber() : this(string.Empty)
    {
    }
}