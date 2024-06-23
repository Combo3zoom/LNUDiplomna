using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Customer;

namespace LightsOn.Application.Customer.Commands.CreateCustomer;

public record CreateCustomerCommand(string Name, string PhoneNumber, string ProblemDescription) : IRequest<int>;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateCustomerCommandHandlerStorageBroker _createCustomerCommandHandlerStorageBroker;

    public CreateCustomerCommandHandler(IApplicationDbContext context,
        ICreateCustomerCommandHandlerStorageBroker createCustomerCommandHandlerStorageBroker,
        ITelegramBot telegramBot)
    {
        _context = context;
        _createCustomerCommandHandlerStorageBroker = createCustomerCommandHandlerStorageBroker;
    }
    
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _createCustomerCommandHandlerStorageBroker.CreateCustomer(request, cancellationToken);
    }
}

public class CreateCustomerCommandHandlerStorageBroker : ICreateCustomerCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandlerStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Customer(request.Name, request.PhoneNumber, request.ProblemDescription); 
        
        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        await _context.Customers.AddAsync(entity, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateCustomerCommandHandlerStorageBroker
{
    Task<int> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken);
}
