namespace LightsOn.Domain.Events.Customer;

public class CustomerDeleteEvent : BaseEvent
{
    public CustomerDeleteEvent(Entities.Customer customer)
    {
        Customer = customer;
    }
    public Entities.Customer Customer { get; }
}