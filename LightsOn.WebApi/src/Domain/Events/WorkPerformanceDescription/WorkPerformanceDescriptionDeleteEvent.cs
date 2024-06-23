namespace LightsOn.Domain.Events.WorkPerformanceDescription;

public class WorkPerformanceDescriptionDeleteEvent : BaseEvent
{
    public WorkPerformanceDescriptionDeleteEvent(Entities.WorkPerformanceDescription workPerformanceDescription)
    {
        WorkPerformanceDescription = workPerformanceDescription;
    }

    public Entities.WorkPerformanceDescription WorkPerformanceDescription { get; }
}