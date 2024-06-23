namespace LightsOn.Domain.Events.Customer;

public class CustomerCompletedEvent : BaseEvent
{
    public CustomerCompletedEvent(Entities.Customer customer)
    {
        Customer = customer;
    }
    public Entities.Customer Customer { get; }
}