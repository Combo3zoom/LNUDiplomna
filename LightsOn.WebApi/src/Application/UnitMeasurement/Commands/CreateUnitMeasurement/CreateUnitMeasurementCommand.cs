using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.UnitMeasurement;

namespace LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;

public record CreateUnitMeasurementCommand(string Name) : IRequest<int>;

public class CreateUnitMeasurementCommandHandler : IRequestHandler<CreateUnitMeasurementCommand, int>
{
    private readonly IApplicationDbContext _context;

    private readonly ICreateUnitMeasurementCommandHandlerStorageBroker
        _createUnitMeasurementCommandHandlerStorageBroker;

    public CreateUnitMeasurementCommandHandler(IApplicationDbContext context,
        ICreateUnitMeasurementCommandHandlerStorageBroker createUnitMeasurementCommandHandlerStorageBroker)
    {
        _context = context;
        _createUnitMeasurementCommandHandlerStorageBroker = createUnitMeasurementCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        return await _createUnitMeasurementCommandHandlerStorageBroker.CreateUnitMeasurement(request,
            cancellationToken);
    }
}

public class CreateUnitMeasurementCommandHandlerStorageBroker : ICreateUnitMeasurementCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateUnitMeasurementCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;

    public async Task<int> CreateUnitMeasurement(CreateUnitMeasurementCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.UnitMeasurement(request.Name);
        
        entity.AddDomainEvent(new UnitMeasurementCreatedEvent(entity));

        await _context.UnitMeasurements.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateUnitMeasurementCommandHandlerStorageBroker
{
    public Task<int> CreateUnitMeasurement(CreateUnitMeasurementCommand request,
        CancellationToken cancellationToken);
}