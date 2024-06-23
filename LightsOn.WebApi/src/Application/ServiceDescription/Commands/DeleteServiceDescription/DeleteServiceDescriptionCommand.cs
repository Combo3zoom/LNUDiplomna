using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.ServiceDescription.Commands.DeleteServiceDescription;

public record DeleteServiceDescriptionCommand(int Id) : IRequest;

public class DeleteServiceDescriptionCommandHandler : IRequestHandler<DeleteServiceDescriptionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteServiceDescriptionCommandHandlerStorageBroker _deleteServiceDescriptionCommandHandlerStorageBroker;

    public DeleteServiceDescriptionCommandHandler(IApplicationDbContext context,
        IDeleteServiceDescriptionCommandHandlerStorageBroker deleteServiceDescriptionCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteServiceDescriptionCommandHandlerStorageBroker = deleteServiceDescriptionCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteServiceDescriptionCommand request, CancellationToken cancellationToken)
    {
        await _deleteServiceDescriptionCommandHandlerStorageBroker.DeleteServiceDescription(request, cancellationToken);
    }
}

public class DeleteServiceDescriptionCommandHandlerStorageBroker : IDeleteServiceDescriptionCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteServiceDescriptionCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteServiceDescription(DeleteServiceDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ServiceDescriptions
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        _context.ServiceDescriptions.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteServiceDescriptionCommandHandlerStorageBroker
{
    Task DeleteServiceDescription(DeleteServiceDescriptionCommand request, CancellationToken cancellationToken);
}