namespace LightsOn.Domain.Entities;

public class Material : BaseEntity
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string? Model { get; set; }
    public decimal Cost { get; set; }

    public int UnitMeasurementId { get; set; }
    public UnitMeasurement UnitMeasurement { get; set; }
    
    public int EstimateId { get; set; }
    public IList<Estimate> Estimates { get; private set; } = null!;

    public Material(string fullName, string shortName, string model, decimal cost, UnitMeasurement unitMeasurement)
    {
        FullName = fullName;
        ShortName = shortName;
        Cost = cost;
        Model = model;
        UnitMeasurement = unitMeasurement;
    }
    private Material()
    {
        FullName = string.Empty;
        ShortName = string.Empty;
        Model = null;
        Cost = Decimal.Zero;
        UnitMeasurement = null!;
        Estimates = new List<Estimate>();
    }
}