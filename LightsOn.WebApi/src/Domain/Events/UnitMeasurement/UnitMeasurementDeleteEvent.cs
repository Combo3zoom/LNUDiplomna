namespace LightsOn.Domain.Events.UnitMeasurement;

public class UnitMeasurementDeleteEvent : BaseEvent
{
    public UnitMeasurementDeleteEvent(Entities.UnitMeasurement unitMeasurement)
    {
        UnitMeasurement = unitMeasurement;
    }

    public Entities.UnitMeasurement UnitMeasurement { get; }
}