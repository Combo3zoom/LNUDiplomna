using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Engine.Commands.UpdateEngine;

public record UpdateEngineCommand(int Id, string Name, string SerialNumber) : IRequest;

public class UpdateEngineCommandHandler : IRequestHandler<UpdateEngineCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateEngineCommandHandlerStorageBroker _updateEngineCommandHandlerStorageBroker;

    public UpdateEngineCommandHandler(IApplicationDbContext context,
        IUpdateEngineCommandHandlerStorageBroker updateEngineCommandHandlerStorageBroker)
    {
        _context = context;
        _updateEngineCommandHandlerStorageBroker = updateEngineCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateEngineCommand request, CancellationToken cancellationToken)
    {
        var engineWithSameSerialNumber = await _updateEngineCommandHandlerStorageBroker
            .GetEngineBySerialNumber(request.SerialNumber, cancellationToken);
        
        if (engineWithSameSerialNumber != null && engineWithSameSerialNumber.Id != request.Id)
            throw new Exception($"'{request.SerialNumber}' must be unique.");

        await _updateEngineCommandHandlerStorageBroker.UpdateEngineCommandHandler(request, cancellationToken);
    }

}

public class UpdateEngineCommandHandlerStorageBroker : IUpdateEngineCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateEngineCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateEngineCommandHandler(UpdateEngineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Engines
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.SerialNumber = request.SerialNumber;

        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Domain.Entities.Engine?> GetEngineBySerialNumber(string serialNumber,
        CancellationToken cancellationToken)
    {
        return await _context.Engines
            .FirstOrDefaultAsync(engine => engine.SerialNumber == serialNumber, cancellationToken);
    }
}

public interface IUpdateEngineCommandHandlerStorageBroker
{
    Task UpdateEngineCommandHandler(UpdateEngineCommand request, CancellationToken cancellationToken);

    public Task<Domain.Entities.Engine?> GetEngineBySerialNumber(string serialNumber,
        CancellationToken cancellationToken);
}