namespace LightsOn.Domain.Events.Engine;

public class EngineCompletedEvent : BaseEvent
{
    public EngineCompletedEvent(Entities.Engine engine)
    {
        Engine = engine;
    }

    public Entities.Engine Engine { get; }
}