using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.UnitMeasurement;

namespace LightsOn.Application.UnitMeasurement.Commands.DeleteUnitMeasurement;

public record DeleteUnitMeasurementCommand(int Id) : IRequest;

public class DeleteUnitMeasurementCommandHandler : IRequestHandler<DeleteUnitMeasurementCommand>
{
    private readonly IApplicationDbContext _context;

    private readonly IDeleteUnitMeasurementCommandHandlerStorageBroker
        _deleteUnitMeasurementCommandHandlerStorageBroker;

    public DeleteUnitMeasurementCommandHandler(IApplicationDbContext context,
        IDeleteUnitMeasurementCommandHandlerStorageBroker deleteUnitMeasurementCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteUnitMeasurementCommandHandlerStorageBroker = deleteUnitMeasurementCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        await _deleteUnitMeasurementCommandHandlerStorageBroker.DeleteUnitMeasurement(request, cancellationToken);
    }
}

public class DeleteUnitMeasurementCommandHandlerStorageBroker : IDeleteUnitMeasurementCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteUnitMeasurementCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteUnitMeasurement(DeleteUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UnitMeasurements
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.UnitMeasurements.Remove(entity);
        
        entity.AddDomainEvent(new UnitMeasurementDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteUnitMeasurementCommandHandlerStorageBroker
{
    public Task DeleteUnitMeasurement(DeleteUnitMeasurementCommand request, CancellationToken cancellationToken);
}