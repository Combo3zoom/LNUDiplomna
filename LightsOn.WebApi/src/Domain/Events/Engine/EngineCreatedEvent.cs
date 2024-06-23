namespace LightsOn.Domain.Events.Engine;

public class EngineCreatedEvent : BaseEvent
{
    public EngineCreatedEvent(Entities.Engine engine)
    {
        Engine = engine;
    }

    public Entities.Engine Engine { get; }
}