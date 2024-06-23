namespace LightsOn.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public string DescribeProblem { get; set; }

    public Customer(string name, string phoneNumber, string describeProblem)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        DescribeProblem = describeProblem;
    }

    private Customer()
    {
        Name = string.Empty;
        PhoneNumber = string.Empty;
        DescribeProblem = string.Empty;
    }
}