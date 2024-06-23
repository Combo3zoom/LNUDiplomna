namespace LightsOn.Domain.Events.Client;

public class ClientCompletedEvent : BaseEvent
{
    public ClientCompletedEvent(Entities.Client client)
    {
        Client = client;
    }
    public Entities.Client Client { get; }
}