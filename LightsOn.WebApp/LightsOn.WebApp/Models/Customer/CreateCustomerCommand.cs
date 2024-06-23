namespace LightsOn.WebApp.Models.Customer;

public record CreateCustomerCommand(string Name, string PhoneNumber, string ProblemDescription);