using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Client.Commands.UpdateClient;

public record UpdateClientCommand(int Id, string Name) : IRequest;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateClientCommandHandlerStorageBroker _updateClientCommandHandlerStorageBroker;

    public UpdateClientCommandHandler(IApplicationDbContext context,
        IUpdateClientCommandHandlerStorageBroker updateClientCommandHandlerStorageBroker)
    {
        _context = context;
        _updateClientCommandHandlerStorageBroker = updateClientCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        await _updateClientCommandHandlerStorageBroker.UpdateClient(request, cancellationToken);
    }
}

public class UpdateClientCommandHandlerStorageBroker : IUpdateClientCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateClientCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateClient(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Clients
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateClientCommandHandlerStorageBroker
{
    Task UpdateClient(UpdateClientCommand request, CancellationToken cancellationToken);
}