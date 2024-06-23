using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Client;

namespace LightsOn.Application.Client.Commands.CreateClient;

public record CreateClientCommand(string Name) : IRequest<int>;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateClientCommandHandlerStorageBroker _createClientCommandHandlerStorageBroker;

    public CreateClientCommandHandler(IApplicationDbContext context,
        ICreateClientCommandHandlerStorageBroker createClientCommandHandlerStorageBroker)
    {
        _context = context;
        _createClientCommandHandlerStorageBroker = createClientCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        return await _createClientCommandHandlerStorageBroker.CreateClient(request, cancellationToken);
    }
}

public class CreateClientCommandHandlerStorageBroker : ICreateClientCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateClientCommandHandlerStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateClient(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Client(request.Name); 
        
        entity.AddDomainEvent(new ClientCreatedEvent(entity));

        _context.Clients.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateClientCommandHandlerStorageBroker
{
    Task<int> CreateClient(CreateClientCommand request, CancellationToken cancellationToken);
}

