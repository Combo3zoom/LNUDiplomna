namespace LightsOn.Domain.Events.PowerEquipment;

public class PowerEquipmentCreatedEvent : BaseEvent
{
    public PowerEquipmentCreatedEvent(Entities.PowerEquipment powerEquipment)
    {
        PowerEquipment = powerEquipment;
    }

    public Entities.PowerEquipment PowerEquipment { get; }
}