namespace LightsOn.Domain.Events.Estimate;

public class EstimateDeleteEvent : BaseEvent
{
    public EstimateDeleteEvent(Entities.Estimate estimate)
    {
        Estimate = estimate;
    }

    public Entities.Estimate Estimate { get; }
}