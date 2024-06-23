using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Customer.Commands.UpdateCustomer;

public record UpdateCustomerCommand(int Id, string Name, string PhoneNumber) : IRequest;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateCustomerCommandHandlerStorageBroker _updateCustomerCommandHandlerStorageBroker;

    public UpdateCustomerCommandHandler(IApplicationDbContext context,
        IUpdateCustomerCommandHandlerStorageBroker updateCustomerCommandHandlerStorageBroker)
    {
        _context = context;
        _updateCustomerCommandHandlerStorageBroker = updateCustomerCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _updateCustomerCommandHandlerStorageBroker.UpdateCustomer(request, cancellationToken);
    }
}

public class UpdateCustomerCommandHandlerStorageBroker : IUpdateCustomerCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateCustomer(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.PhoneNumber = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateCustomerCommandHandlerStorageBroker
{
    Task UpdateCustomer(UpdateCustomerCommand request, CancellationToken cancellationToken);
}