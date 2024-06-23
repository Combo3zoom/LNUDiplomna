namespace LightsOn.Domain.Entities;

public class Client : BaseAuditableEntity
{
    public string Name { get; set; }

    public Client(string name) => Name = name;
    
    private Client() => Name = string.Empty;
    
}