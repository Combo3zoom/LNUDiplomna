namespace LightsOn.Domain.Events.PowerEquipment;

public class PowerEquipmentCompletedEvent : BaseEvent
{
    public PowerEquipmentCompletedEvent(Entities.PowerEquipment powerEquipment)
    {
        PowerEquipment = powerEquipment;
    }

    public Entities.PowerEquipment PowerEquipment { get; }
}