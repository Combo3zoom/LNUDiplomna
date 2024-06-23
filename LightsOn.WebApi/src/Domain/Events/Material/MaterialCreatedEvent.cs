namespace LightsOn.Domain.Events.Material;

public class MaterialCreatedEvent : BaseEvent
{
    public MaterialCreatedEvent(Entities.Material material)
    {
        Material = material;
    }

    public Entities.Material Material { get; }
}