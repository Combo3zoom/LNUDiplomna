using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Engine;

namespace LightsOn.Application.Engine.Commands.CreateEngine;

public record CreateEngineCommand(string Name, string SerialNumber) : IRequest<int>;

public class CreateEngineCommandHandler : IRequestHandler<CreateEngineCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateEngineCommandHandlerStorageBroker _createEngineCommandHandlerStorageBroker;

    public CreateEngineCommandHandler(IApplicationDbContext context,
        ICreateEngineCommandHandlerStorageBroker createEngineCommandHandlerStorageBroker)
    {
        _context = context;
        _createEngineCommandHandlerStorageBroker = createEngineCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateEngineCommand request, CancellationToken cancellationToken)
    {
        var beUniqueSerialNumber = await _createEngineCommandHandlerStorageBroker
            .BeUniqueSerialNumber(request.SerialNumber, cancellationToken);

        if (beUniqueSerialNumber is false)
            throw new Exception($"'{request.SerialNumber}' must be unique.");

        return await _createEngineCommandHandlerStorageBroker.CreateEngineCommandHandler(request, cancellationToken);
    }
}

public class CreateEngineCommandHandlerStorageBroker : ICreateEngineCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateEngineCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task<int> CreateEngineCommandHandler(CreateEngineCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Engine(request.Name, request.SerialNumber);
        
        entity.AddDomainEvent(new EngineCreatedEvent(entity));

        _context.Engines.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
    
    public async Task<bool> BeUniqueSerialNumber(string serialNumber, CancellationToken cancellationToken)
    {
        return await _context.Engines
            .AllAsync(engine => engine.SerialNumber != serialNumber, cancellationToken);
    }
}

public interface ICreateEngineCommandHandlerStorageBroker
{
    public Task<int> CreateEngineCommandHandler(CreateEngineCommand request, CancellationToken cancellationToken);
    public Task<bool> BeUniqueSerialNumber(string serialNumber, CancellationToken cancellationToken);
}