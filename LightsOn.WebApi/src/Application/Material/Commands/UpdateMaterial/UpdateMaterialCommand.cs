using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LightsOn.Application.Material.Commands.UpdateMaterial;

public record UpdateMaterialCommand(int Id, string FullName, string ShortName, string? Model,
    decimal Cost, UpdateUnitMeasurementForUpdateMaterialCommand UnitMeasurementForUpdateMaterialCommand) : IRequest;

public record UpdateUnitMeasurementForUpdateMaterialCommand(string Name);
public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateMaterialCommandHandlerStorageBroker _updateMaterialCommandHandlerStorageBroker;

    public UpdateMaterialCommandHandler(IApplicationDbContext context,
        IUpdateMaterialCommandHandlerStorageBroker updateMaterialCommandHandlerStorageBroker)
    {
        _context = context;
        _updateMaterialCommandHandlerStorageBroker = updateMaterialCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        await _updateMaterialCommandHandlerStorageBroker.UpdateMaterial(request, cancellationToken);
    }
}

public class UpdateMaterialCommandHandlerStorageBroker : IUpdateMaterialCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateMaterialCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateMaterial(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var unitMeasurement = new Domain.Entities.UnitMeasurement(request.UnitMeasurementForUpdateMaterialCommand.Name);
        var entity = await _context.Materials
            .Where(material => material.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.FullName = request.FullName;
        entity.ShortName = request.ShortName;
        entity.Cost = request.Cost;
        entity.Model = request.Model;
        entity.UnitMeasurement = unitMeasurement;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateMaterialCommandHandlerStorageBroker
{
    public Task UpdateMaterial(UpdateMaterialCommand request, CancellationToken cancellationToken);
}