using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.PowerEquipment;

namespace LightsOn.Application.PowerEquipment.Commands.DeletePowerEquipment;

public record DeletePowerEquipmentCommand(int Id) : IRequest;

public class DeletePowerEquipmentCommandHandler : IRequestHandler<DeletePowerEquipmentCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeletePowerEquipmentCommandHandlerStorageBroker _deletePowerEquipmentCommandHandlerStorageBroker;

    public DeletePowerEquipmentCommandHandler(IApplicationDbContext context,
        IDeletePowerEquipmentCommandHandlerStorageBroker deletePowerEquipmentCommandHandlerStorageBroker)
    {
        _context = context;
        _deletePowerEquipmentCommandHandlerStorageBroker = deletePowerEquipmentCommandHandlerStorageBroker;
    }

    public async Task Handle(DeletePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        await _deletePowerEquipmentCommandHandlerStorageBroker.DeletePowerEquipment(request, cancellationToken);
    }
}

public class DeletePowerEquipmentCommandHandlerStorageBroker : IDeletePowerEquipmentCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeletePowerEquipmentCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;

    public async Task DeletePowerEquipment(DeletePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PowerEquipments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.PowerEquipments.Remove(entity);
        
        entity.AddDomainEvent(new PowerEquipmentDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeletePowerEquipmentCommandHandlerStorageBroker
{
    public Task DeletePowerEquipment(DeletePowerEquipmentCommand request, CancellationToken cancellationToken);
}