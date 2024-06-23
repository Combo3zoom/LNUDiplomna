namespace LightsOn.Domain.Entities;

public class UnitMeasurement : BaseEntity
{
    public string Name { get; set; }
    
    public int MaterialsId { get; set; }
    public IList<Material> Materials { get; private set; } = null!;
    public UnitMeasurement(string name) => Name = name;
    
    private UnitMeasurement()
    {
        Name = string.Empty;
        Materials = new List<Material>();
    }
}