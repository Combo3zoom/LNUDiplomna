using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Client;

namespace LightsOn.Application.Client.Commands.DeleteClient;

public record DeleteCommandClient(int Id) : IRequest;

public class DeleteCompanyPhoneNumberCommandHandler : IRequestHandler<DeleteCommandClient>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteClientCommandHandlerStorageBroker _deleteClientCommandHandlerStorageBroker;

    public DeleteCompanyPhoneNumberCommandHandler(IApplicationDbContext context,
        IDeleteClientCommandHandlerStorageBroker deleteClientCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteClientCommandHandlerStorageBroker = deleteClientCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteCommandClient request, CancellationToken cancellationToken)
    {
        await _deleteClientCommandHandlerStorageBroker.DeleteClient(request, cancellationToken);
    }
}

public class DeleteClientCommandHandlerStorageBroker : IDeleteClientCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteClient(DeleteCommandClient request, CancellationToken cancellationToken)
    {
        var entity = await _context.Clients
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        _context.Clients.Remove(entity);
        
        entity.AddDomainEvent(new ClientDeleteEvent(entity)); 

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteClientCommandHandlerStorageBroker
{
    Task DeleteClient(DeleteCommandClient request, CancellationToken cancellationToken);
}