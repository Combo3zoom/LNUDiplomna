namespace LightsOn.Domain.Events.WorkPerformanceDescription;

public class WorkPerformanceDescriptionCreatedEvent : BaseEvent
{
    public WorkPerformanceDescriptionCreatedEvent(Entities.WorkPerformanceDescription workPerformanceDescription)
    {
        WorkPerformanceDescription = workPerformanceDescription;
    }

    public Entities.WorkPerformanceDescription WorkPerformanceDescription { get; }
}