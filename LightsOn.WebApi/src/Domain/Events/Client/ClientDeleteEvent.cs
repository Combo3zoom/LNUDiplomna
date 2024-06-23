namespace LightsOn.Domain.Events.Client;

public class ClientDeleteEvent : BaseEvent
{
    public ClientDeleteEvent(Entities.Client client)
    {
        Client = client;
    }
    public Entities.Client Client { get; }
}