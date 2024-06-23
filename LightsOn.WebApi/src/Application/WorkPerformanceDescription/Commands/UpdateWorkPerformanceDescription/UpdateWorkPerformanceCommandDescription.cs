using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.WorkPerformanceDescription.Commands.UpdateWorkPerformanceDescription;

public record UpdateWorkPerformanceCommandDescription(int Id, Domain.Entities.Client Client,
    Domain.Entities.PowerEquipment PowerEquipment, Domain.Entities.Engine Engine) : IRequest;

public class UpdateWorkPerformanceDescriptionCommandHandler : IRequestHandler<UpdateWorkPerformanceCommandDescription>
{
    private readonly IApplicationDbContext _context;

    private readonly IUpdateWorkPerformanceDescriptionCommandHandlerStorageBroker
        _updateWorkPerformanceDescriptionCommandHandlerStorageBroker;

    public UpdateWorkPerformanceDescriptionCommandHandler(IApplicationDbContext context,
        IUpdateWorkPerformanceDescriptionCommandHandlerStorageBroker updateWorkPerformanceDescriptionCommandHandlerStorageBroker)
    {
        _context = context;
        _updateWorkPerformanceDescriptionCommandHandlerStorageBroker = updateWorkPerformanceDescriptionCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateWorkPerformanceCommandDescription request, CancellationToken cancellationToken)
    {
        await _updateWorkPerformanceDescriptionCommandHandlerStorageBroker.UpdateWorkPerformanceDescription(request,
            cancellationToken);
    }
}

public class UpdateWorkPerformanceDescriptionCommandHandlerStorageBroker
    : IUpdateWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkPerformanceDescriptionCommandHandlerStorageBroker(IApplicationDbContext context)
        => _context = context;
    
    public async Task UpdateWorkPerformanceDescription(UpdateWorkPerformanceCommandDescription request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.WorkPerformanceDescriptions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Client = request.Client;
        entity.PowerEquipment = request.PowerEquipment;
        entity.Engine = request.Engine;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    public Task UpdateWorkPerformanceDescription(UpdateWorkPerformanceCommandDescription request,
        CancellationToken cancellationToken);
}