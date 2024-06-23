using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Estimate.Commands.UpdateEstimate;

public record UpdateEstimateCommand(int Id, uint MaterialsCount, uint UsedMaterialsCount) : IRequest;

public class UpdateEstimateHandlerCommand : IRequestHandler<UpdateEstimateCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateEstimateHandlerCommandStorageBroker _updateEstimateHandlerCommandStorageBroker;

    public UpdateEstimateHandlerCommand(IApplicationDbContext context,
        IUpdateEstimateHandlerCommandStorageBroker updateEstimateHandlerCommandStorageBroker)
    {
        _context = context;
        _updateEstimateHandlerCommandStorageBroker = updateEstimateHandlerCommandStorageBroker;
    }

    public async Task Handle(UpdateEstimateCommand request, CancellationToken cancellationToken)
    {
        await _updateEstimateHandlerCommandStorageBroker.UpdateEstimate(request, cancellationToken);
    }
}

public class UpdateEstimateHandlerCommandStorageBroker : IUpdateEstimateHandlerCommandStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateEstimateHandlerCommandStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task UpdateEstimate(UpdateEstimateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Estimates
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.MaterialsCount = request.MaterialsCount;
        entity.UsedMaterialsCount = request.UsedMaterialsCount;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateEstimateHandlerCommandStorageBroker
{
    public Task UpdateEstimate(UpdateEstimateCommand request, CancellationToken cancellationToken);
}