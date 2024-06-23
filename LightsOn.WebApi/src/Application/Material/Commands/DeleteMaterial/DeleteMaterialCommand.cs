using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Material;

namespace LightsOn.Application.Material.Commands.DeleteMaterial;

public record DeleteMaterialCommand(int Id) : IRequest;

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteMaterialCommandHandlerStorageBroker _deleteMaterialCommandHandlerStorageBroker;

    public DeleteMaterialCommandHandler(IApplicationDbContext context,
        IDeleteMaterialCommandHandlerStorageBroker deleteMaterialCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteMaterialCommandHandlerStorageBroker = deleteMaterialCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        await _deleteMaterialCommandHandlerStorageBroker.DeleteMaterial(request, cancellationToken);
    }
}

public class DeleteMaterialCommandHandlerStorageBroker : IDeleteMaterialCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteMaterialCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteMaterial(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Materials
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Materials.Remove(entity);
        
        entity.AddDomainEvent(new MaterialDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteMaterialCommandHandlerStorageBroker
{
    public Task DeleteMaterial(DeleteMaterialCommand request, CancellationToken cancellationToken);
}