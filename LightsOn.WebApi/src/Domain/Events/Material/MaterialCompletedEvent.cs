namespace LightsOn.Domain.Events.Material;

public class MaterialCompletedEvent : BaseEvent
{
    public MaterialCompletedEvent(Entities.Material material)
    {
        Material = material;
    }

    public Entities.Material Material { get; }
}