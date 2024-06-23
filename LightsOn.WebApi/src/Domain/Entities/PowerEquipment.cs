namespace LightsOn.Domain.Entities;

public class PowerEquipment : BaseEntity
{
    public string Name { get; set; }
    public PowerEquipment(string name)
    {
        Name = name;
    }
    private PowerEquipment()
    {
        Name = string.Empty;
    }
}