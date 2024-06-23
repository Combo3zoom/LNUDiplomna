namespace LightsOn.Domain.Events.PowerEquipment;

public class PowerEquipmentDeleteEvent : BaseEvent
{
    public PowerEquipmentDeleteEvent(Entities.PowerEquipment powerEquipment)
    {
        PowerEquipment = powerEquipment;
    }

    public Entities.PowerEquipment PowerEquipment { get; }
}