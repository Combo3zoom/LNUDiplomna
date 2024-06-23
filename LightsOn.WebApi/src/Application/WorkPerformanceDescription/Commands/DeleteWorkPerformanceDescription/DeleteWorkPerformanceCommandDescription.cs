using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.WorkPerformanceDescription;

namespace LightsOn.Application.WorkPerformanceDescription.Commands.DeleteWorkPerformanceDescription;

public record DeleteWorkPerformanceCommandDescription(int Id) : IRequest;

public class DeleteWorkPerformanceDescriptionCommandHandler : IRequestHandler<DeleteWorkPerformanceCommandDescription>
{
    private readonly IApplicationDbContext _context;

    private readonly IDeleteWorkPerformanceDescriptionCommandHandlerStorageBroker
        _deleteWorkPerformanceDescriptionCommandHandlerStorageBroker;

    public DeleteWorkPerformanceDescriptionCommandHandler(IApplicationDbContext context,
        IDeleteWorkPerformanceDescriptionCommandHandlerStorageBroker deleteWorkPerformanceDescriptionCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteWorkPerformanceDescriptionCommandHandlerStorageBroker = deleteWorkPerformanceDescriptionCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteWorkPerformanceCommandDescription request, CancellationToken cancellationToken)
    {
        await _deleteWorkPerformanceDescriptionCommandHandlerStorageBroker.DeleteWorkPerformanceDescription(request,
            cancellationToken);
    }
}

public class DeleteWorkPerformanceDescriptionCommandHandlerStorageBroker
    : IDeleteWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkPerformanceDescriptionCommandHandlerStorageBroker(IApplicationDbContext context)
        => _context = context;
    
    public async Task DeleteWorkPerformanceDescription(DeleteWorkPerformanceCommandDescription request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.WorkPerformanceDescriptions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.WorkPerformanceDescriptions.Remove(entity);
        
        entity.AddDomainEvent(new WorkPerformanceDescriptionDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteWorkPerformanceDescriptionCommandHandlerStorageBroker
{
    public Task DeleteWorkPerformanceDescription(DeleteWorkPerformanceCommandDescription request,
        CancellationToken cancellationToken);
}