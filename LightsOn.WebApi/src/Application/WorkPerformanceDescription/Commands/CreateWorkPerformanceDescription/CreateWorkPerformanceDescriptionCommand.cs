using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.WorkPerformanceDescription;

namespace LightsOn.Application.WorkPerformanceDescription.Commands.CreateWorkPerformanceDescription;

public record CreateWorkPerformanceDescriptionCommand(Domain.Entities.Client Client,
    Domain.Entities.PowerEquipment PowerEquipment, Domain.Entities.Engine Engine) : IRequest<int>;

public class CreateWorkPerformanceDescriptionCommandHandler : IRequestHandler<CreateWorkPerformanceDescriptionCommand, int>
{
    private readonly IApplicationDbContext _context;

    private readonly ICreateWorkPerformanceDescriptionCommandHandlerStorageBroker
        _createWorkPerformanceDescriptionCommandHandlerStorageBroker;

    public CreateWorkPerformanceDescriptionCommandHandler(IApplicationDbContext context,
        ICreateWorkPerformanceDescriptionCommandHandlerStorageBroker createWorkPerformanceDescriptionCommandHandlerStorageBroker)
    {
        _context = context;
        _createWorkPerformanceDescriptionCommandHandlerStorageBroker = createWorkPerformanceDescriptionCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateWorkPerformanceDescriptionCommand request, CancellationToken cancellationToken)
    {
        return await _createWorkPerformanceDescriptionCommandHandlerStorageBroker.CreateWorkPerformanceDescription(
            request, cancellationToken);
    }
}

public class CreateWorkPerformanceDescriptionCommandHandlerStorageBroker
    : ICreateWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateWorkPerformanceDescriptionCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;

    public async Task<int> CreateWorkPerformanceDescription(CreateWorkPerformanceDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.WorkPerformanceDescription(request.Client, request.PowerEquipment,
            request.Engine);
        
        entity.AddDomainEvent(new WorkPerformanceDescriptionCreatedEvent(entity));

        await _context.WorkPerformanceDescriptions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    public Task<int> CreateWorkPerformanceDescription(CreateWorkPerformanceDescriptionCommand request,
        CancellationToken cancellationToken);
}