using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Engine;

namespace LightsOn.Application.Engine.Commands.DeleteEngine;

public record DeleteEngineCommand(int Id) : IRequest;

public class DeleteEngineCommandHandle : IRequestHandler<DeleteEngineCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteEngineCommandHandleStorageBroker _deleteEngineCommandHandleStorageBroker;

    public DeleteEngineCommandHandle(IApplicationDbContext context,
        IDeleteEngineCommandHandleStorageBroker deleteEngineCommandHandleStorageBroker)
    {
        _context = context;
        _deleteEngineCommandHandleStorageBroker = deleteEngineCommandHandleStorageBroker;
    }

    public async Task Handle(DeleteEngineCommand request, CancellationToken cancellationToken)
    {
        await _deleteEngineCommandHandleStorageBroker.DeleteEngine(request, cancellationToken);
    }
}

public class DeleteEngineCommandHandleStorageBroker : IDeleteEngineCommandHandleStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteEngineCommandHandleStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DeleteEngine(DeleteEngineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Engines
            .FindAsync(new object?[] { request.Id });

        Guard.Against.NotFound(request.Id, entity);

        _context.Engines.Remove(entity);
        
        entity.AddDomainEvent(new EngineDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteEngineCommandHandleStorageBroker
{
    Task DeleteEngine(DeleteEngineCommand request, CancellationToken cancellationToken);
}