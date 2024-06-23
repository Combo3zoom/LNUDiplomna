namespace LightsOn.Domain.Events.Material;

public class MaterialDeleteEvent : BaseEvent
{
    public MaterialDeleteEvent(Entities.Material material)
    {
        Material = material;
    }

    public Entities.Material Material { get; }
}