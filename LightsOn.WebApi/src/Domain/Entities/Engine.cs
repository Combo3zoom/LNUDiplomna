namespace LightsOn.Domain.Entities;

public class Engine : BaseEntity
{
    public string Name {get; set;}
    public string SerialNumber {get; set;}

    public Engine(string name, string serialNumber)
    {
        Name = name;
        SerialNumber = serialNumber;
    }
    private Engine()
    {
        Name = String.Empty;
        SerialNumber = String.Empty;
    }
}