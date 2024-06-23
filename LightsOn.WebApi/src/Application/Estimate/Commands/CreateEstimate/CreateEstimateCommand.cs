using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Entities;
using LightsOn.Domain.Events.Estimate;

namespace LightsOn.Application.Estimate.Commands.CreateEstimate;

public record CreateEstimateCommand(CreateCategoryExpenseForEstimateCommand CategoryExpense,
    CreateMaterialCommandForEstimateCommand Material,
    uint MaterialsCount, uint UsedMaterialsCount) : IRequest<int>;

public record CreateCategoryExpenseForEstimateCommand(string Name);
public record CreateMaterialCommandForEstimateCommand(string FullName, string ShortName, string Model,
    decimal Cost, CreateUnitMeasurementForCreateMaterialCommand UnitMeasurementForCreateMaterialCommand) : IRequest<int>;

public record CreateUnitMeasurementForCreateMaterialCommand(string Name);

public class CreateEstimateCommandHandler : IRequestHandler<CreateEstimateCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateEstimateCommandHandlerStorageBroker _createEstimateCommandHandlerStorageBroker;

    public CreateEstimateCommandHandler(IApplicationDbContext context,
        ICreateEstimateCommandHandlerStorageBroker createEstimateCommandHandlerStorageBroker)
    {
        _context = context;
        _createEstimateCommandHandlerStorageBroker = createEstimateCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateEstimateCommand request, CancellationToken cancellationToken)
    {
        return await _createEstimateCommandHandlerStorageBroker.CreateEstimate(request, cancellationToken);
    }
}

public class CreateEstimateCommandHandlerStorageBroker : ICreateEstimateCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateEstimateCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task<int> CreateEstimate(CreateEstimateCommand request, CancellationToken cancellationToken)
    {
        var unitMeasurement = new Domain.Entities
            .UnitMeasurement(request.Material.UnitMeasurementForCreateMaterialCommand.Name);
        var categoryExpense = new Domain.Entities.CategoryExpense(request.CategoryExpense.Name);
        var material = new Domain.Entities.Material(request.Material.FullName, request.Material.ShortName,
            request.Material.Model, request.Material.Cost, unitMeasurement);
        
        var entity = new Domain.Entities.Estimate(categoryExpense, material, request.MaterialsCount, 
            request.UsedMaterialsCount);
        
        entity.AddDomainEvent(new EstimateCreatedEvent(entity));

        _context.Estimates.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateEstimateCommandHandlerStorageBroker
{
    public Task<int> CreateEstimate(CreateEstimateCommand request, CancellationToken cancellationToken);
}