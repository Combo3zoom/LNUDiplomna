namespace LightsOn.Domain.Events.Engine;

public class EngineDeleteEvent : BaseEvent
{
    public EngineDeleteEvent(Entities.Engine engine)
    {
        Engine = engine;
    }

    public Entities.Engine Engine { get; }
}