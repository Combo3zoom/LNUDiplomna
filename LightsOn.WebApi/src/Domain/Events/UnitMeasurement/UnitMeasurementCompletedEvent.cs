namespace LightsOn.Domain.Events.UnitMeasurement;

public class UnitMeasurementCompletedEvent : BaseEvent
{
    public UnitMeasurementCompletedEvent(Entities.UnitMeasurement unitMeasurement)
    {
        UnitMeasurement = unitMeasurement;
    }

    public Entities.UnitMeasurement UnitMeasurement { get; }
}