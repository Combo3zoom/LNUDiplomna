namespace LightsOn.Domain.Entities;

public class WorkPerformanceDescription : BaseAuditableEntity
{
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int PowerEquipmentId { get; set; }
    public PowerEquipment PowerEquipment { get; set; }
    public int EngineId { get; set; }
    public Engine Engine { get; set; }
    public WorkPerformanceDescription(Client client, PowerEquipment powerEquipment, Engine engine)
    {
        Client = client;
        PowerEquipment = powerEquipment;
        Engine = engine;
    }
    private WorkPerformanceDescription()
    {
        Client = null!;
        PowerEquipment = null!;
        Engine = null!;
    }
}