using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Estimate.Commands.DeleteEstimate;

public record DeleteEstimateCommand(int Id) : IRequest;

public class DeleteEstimateCommandHandler : IRequestHandler<DeleteEstimateCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteEstimateCommandHandlerStorageBroker _deleteEstimateCommandHandlerStorageBroker;

    public DeleteEstimateCommandHandler(IApplicationDbContext context,
        IDeleteEstimateCommandHandlerStorageBroker deleteEstimateCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteEstimateCommandHandlerStorageBroker = deleteEstimateCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteEstimateCommand request, CancellationToken cancellationToken)
    {
        await _deleteEstimateCommandHandlerStorageBroker.DeleteEstimate(request, cancellationToken);
    }
}

public class DeleteEstimateCommandHandlerStorageBroker: IDeleteEstimateCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteEstimateCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteEstimate(DeleteEstimateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Estimates
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Estimates.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteEstimateCommandHandlerStorageBroker
{
    public Task DeleteEstimate(DeleteEstimateCommand request, CancellationToken cancellationToken);
}