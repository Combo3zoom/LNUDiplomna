using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Customer;

namespace LightsOn.Application.Customer.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteCustomerCommandHandlerStorageBroker _deleteCustomerCommandHandlerStorageBroker;

    public DeleteCustomerCommandHandler(IApplicationDbContext context,
        IDeleteCustomerCommandHandlerStorageBroker deleteCustomerCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteCustomerCommandHandlerStorageBroker = deleteCustomerCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await _deleteCustomerCommandHandlerStorageBroker.DeleteCustomer(request, cancellationToken);
    }
}

public class DeleteCustomerCommandHandlerStorageBroker : IDeleteCustomerCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteCustomer(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        _context.Customers.Remove(entity);
        
        entity.AddDomainEvent(new CustomerDeleteEvent(entity)); 

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteCustomerCommandHandlerStorageBroker
{
    Task DeleteCustomer(DeleteCustomerCommand request, CancellationToken cancellationToken);
}