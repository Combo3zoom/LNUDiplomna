using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.ServiceDescription.Commands.CreateServiceDescription;

public record CreateServiceDescriptionCommand(string HeaderText, string MainText, string LowerPriceLimit) : IRequest<int>;

public class CreateServiceDescriptionCommandHandler : IRequestHandler<CreateServiceDescriptionCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateServiceDescriptionCommandHandlerStorageBroker _createServiceDescriptionHandlerStorageBroker;

    public CreateServiceDescriptionCommandHandler(IApplicationDbContext context,
        ICreateServiceDescriptionCommandHandlerStorageBroker createServiceDescriptionHandlerStorageBroker)
    {
        _context = context;
        _createServiceDescriptionHandlerStorageBroker = createServiceDescriptionHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateServiceDescriptionCommand request, CancellationToken cancellationToken)
    {
        return await _createServiceDescriptionHandlerStorageBroker.CreateServiceDescription(request, cancellationToken);
    }
}

public class CreateServiceDescriptionCommandHandlerStorageBroker : ICreateServiceDescriptionCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateServiceDescriptionCommandHandlerStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateServiceDescription(CreateServiceDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ServiceDescription(request.HeaderText, request.MainText, request.LowerPriceLimit); 

        _context.ServiceDescriptions.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateServiceDescriptionCommandHandlerStorageBroker
{
    Task<int> CreateServiceDescription(CreateServiceDescriptionCommand request, CancellationToken cancellationToken);
}
