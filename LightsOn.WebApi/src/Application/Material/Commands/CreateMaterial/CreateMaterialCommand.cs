using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Entities;
using LightsOn.Domain.Events.Material;

namespace LightsOn.Application.Material.Commands.CreateMaterial;

public record CreateMaterialCommand(string FullName, string ShortName, string Model,
    decimal Cost, CreateUnitMeasurementForCreateMaterialCommand UnitMeasurementForCreateMaterialCommand) : IRequest<int>;

public record CreateUnitMeasurementForCreateMaterialCommand(string Name);

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateMaterialCommandHandlerStorageBroker _createMaterialCommandHandlerStorageBroker;

    public CreateMaterialCommandHandler(IApplicationDbContext context,
        ICreateMaterialCommandHandlerStorageBroker createMaterialCommandHandlerStorageBroker)
    {
        _context = context;
        _createMaterialCommandHandlerStorageBroker = createMaterialCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        return await _createMaterialCommandHandlerStorageBroker.CreateMaterial(request, cancellationToken);
    }
}

public class CreateMaterialCommandHandlerStorageBroker : ICreateMaterialCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateMaterialCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task<int> CreateMaterial(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var unitMeasurement = new Domain.Entities.UnitMeasurement(request.UnitMeasurementForCreateMaterialCommand.Name);
        var entity = new Domain.Entities.Material(request.FullName, request.ShortName, request.Model, request.Cost, 
            unitMeasurement);
        
        entity.AddDomainEvent(new MaterialCreatedEvent(entity));

        await _context.Materials.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateMaterialCommandHandlerStorageBroker
{
    public Task<int> CreateMaterial(CreateMaterialCommand request, CancellationToken cancellationToken);
}