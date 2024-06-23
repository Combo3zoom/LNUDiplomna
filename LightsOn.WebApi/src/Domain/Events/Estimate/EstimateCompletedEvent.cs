namespace LightsOn.Domain.Events.Estimate;

public class EstimateCompletedEvent : BaseEvent
{
    public EstimateCompletedEvent(Entities.Estimate estimate)
    {
        Estimate = estimate;
    }

    public Entities.Estimate Estimate { get; }
}