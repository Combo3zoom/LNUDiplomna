using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.PowerEquipment;

namespace LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;

public record CreatePowerEquipmentCommand(string Name) : IRequest<int>;

public class CreatePowerEquipmentCommandHandler : IRequestHandler<CreatePowerEquipmentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreatePowerEquipmentQueryHandlerStorageBroker _createPowerEquipmentQueryHandlerStorageBroker;

    public CreatePowerEquipmentCommandHandler(IApplicationDbContext context,
        ICreatePowerEquipmentQueryHandlerStorageBroker createPowerEquipmentQueryHandlerStorageBroker)
    {
        _context = context;
        _createPowerEquipmentQueryHandlerStorageBroker = createPowerEquipmentQueryHandlerStorageBroker;
    }

    public async Task<int> Handle(CreatePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        return await _createPowerEquipmentQueryHandlerStorageBroker.CreatePowerEquipment(request, cancellationToken);
    }
}

public class CreatePowerEquipmentQueryHandlerStorageBroker : ICreatePowerEquipmentQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreatePowerEquipmentQueryHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task<int> CreatePowerEquipment(CreatePowerEquipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.PowerEquipment(request.Name);
        
        entity.AddDomainEvent(new PowerEquipmentCreatedEvent(entity));

        await _context.PowerEquipments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreatePowerEquipmentQueryHandlerStorageBroker
{
    public Task<int> CreatePowerEquipment(CreatePowerEquipmentCommand request, CancellationToken cancellationToken);
}