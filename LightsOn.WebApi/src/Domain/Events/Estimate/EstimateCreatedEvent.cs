namespace LightsOn.Domain.Events.Estimate;

public class EstimateCreatedEvent : BaseEvent
{
    public EstimateCreatedEvent(Entities.Estimate estimate)
    {
        Estimate = estimate;
    }

    public Entities.Estimate Estimate { get; }
}