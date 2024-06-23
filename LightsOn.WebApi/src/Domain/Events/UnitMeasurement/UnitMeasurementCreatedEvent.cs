namespace LightsOn.Domain.Events.UnitMeasurement;

public class UnitMeasurementCreatedEvent : BaseEvent
{
    public UnitMeasurementCreatedEvent(Entities.UnitMeasurement unitMeasurement)
    {
        UnitMeasurement = unitMeasurement;
    }

    public Entities.UnitMeasurement UnitMeasurement { get; }
}