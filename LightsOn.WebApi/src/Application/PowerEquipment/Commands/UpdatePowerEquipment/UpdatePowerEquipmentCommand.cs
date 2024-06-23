using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.PowerEquipment.Commands.UpdatePowerEquipment;

public record UpdatePowerEquipmentCommand(int Id, string Name) : IRequest;

public class UpdatePowerEquipmentCommandHandler : IRequestHandler<UpdatePowerEquipmentCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdatePowerEquipmentCommandHandlerStorageBroker _updatePowerEquipmentCommandHandlerStorageBroker;

    public UpdatePowerEquipmentCommandHandler(IApplicationDbContext context,
        IUpdatePowerEquipmentCommandHandlerStorageBroker updatePowerEquipmentCommandHandlerStorageBroker)
    {
        _context = context;
        _updatePowerEquipmentCommandHandlerStorageBroker = updatePowerEquipmentCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdatePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PowerEquipments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class UpdatePowerEquipmentCommandHandlerStorageBroker : IUpdatePowerEquipmentCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdatePowerEquipmentCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;

    public async Task UpdatePowerEquipment(UpdatePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PowerEquipments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdatePowerEquipmentCommandHandlerStorageBroker
{
    public Task UpdatePowerEquipment(UpdatePowerEquipmentCommand request, CancellationToken cancellationToken);
}