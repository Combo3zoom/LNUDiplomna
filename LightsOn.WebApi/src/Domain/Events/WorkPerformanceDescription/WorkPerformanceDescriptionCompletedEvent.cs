namespace LightsOn.Domain.Events.WorkPerformanceDescription;

public class WorkPerformanceDescriptionCompletedEvent : BaseEvent
{
    public WorkPerformanceDescriptionCompletedEvent(Entities.WorkPerformanceDescription workPerformanceDescription)
    {
        WorkPerformanceDescription = workPerformanceDescription;
    }

    public Entities.WorkPerformanceDescription WorkPerformanceDescription { get; }
}