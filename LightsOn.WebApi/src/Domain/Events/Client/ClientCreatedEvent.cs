namespace LightsOn.Domain.Events.Client;

public class ClientCreatedEvent : BaseEvent
{
    public ClientCreatedEvent(Entities.Client client)
    {
        Client = client;
    }
    public Entities.Client Client { get; }
}