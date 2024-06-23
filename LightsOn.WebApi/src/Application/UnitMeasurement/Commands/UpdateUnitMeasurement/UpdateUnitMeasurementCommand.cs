using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.UnitMeasurement.Commands.UpdateUnitMeasurement;

public record UpdateUnitMeasurementCommand(int Id, string Name) : IRequest;

public class UpdateUnitMeasurementCommandHandler : IRequestHandler<UpdateUnitMeasurementCommand>
{
    private readonly IApplicationDbContext _context;

    private readonly IUpdateUnitMeasurementCommandHandlerStorageBroker
        _updateUnitMeasurementCommandHandlerStorageBroker;

    public UpdateUnitMeasurementCommandHandler(IApplicationDbContext context,
        IUpdateUnitMeasurementCommandHandlerStorageBroker updateUnitMeasurementCommandHandlerStorageBroker)
    {
        _context = context;
        _updateUnitMeasurementCommandHandlerStorageBroker = updateUnitMeasurementCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        await _updateUnitMeasurementCommandHandlerStorageBroker.UpdateUnitMeasurement(request, cancellationToken);
    }
}

public class UpdateUnitMeasurementCommandHandlerStorageBroker : IUpdateUnitMeasurementCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateUnitMeasurementCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateUnitMeasurement(UpdateUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UnitMeasurements
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateUnitMeasurementCommandHandlerStorageBroker
{
    public Task UpdateUnitMeasurement(UpdateUnitMeasurementCommand request, CancellationToken cancellationToken);
}