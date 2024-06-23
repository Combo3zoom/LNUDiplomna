using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Client.Queries.GetByIdClient;

public record GetByIdClient(int Id) : IRequest<ClientBriefDto>;

public class GetByIdClientHandler : IRequestHandler<GetByIdClient, ClientBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdClientStorageBroker _getByIdClientStorageBroker;

    public GetByIdClientHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdClientStorageBroker getByIdClientStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdClientStorageBroker = getByIdClientStorageBroker;
    }

    public Task<ClientBriefDto> Handle(GetByIdClient request, CancellationToken cancellationToken)
    {
        var clientBriefDto = _getByIdClientStorageBroker.GetByIdClient(request, cancellationToken);

        if (clientBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(_context.Clients));

        return Task.FromResult(clientBriefDto);
    }
}

public class GetByIdClientStorageBroker : IGetByIdClientStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdClientStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public ClientBriefDto? GetByIdClient(GetByIdClient request, CancellationToken cancellationToken)
    {
        var clientBriefDto = _context.Clients
            .Where(client => client.Id == request.Id);
        
        var a = clientBriefDto.ProjectTo<ClientBriefDto>(_mapper.ConfigurationProvider);
        
        return a.SingleOrDefault();
    }
}

public interface IGetByIdClientStorageBroker
{
    public ClientBriefDto? GetByIdClient(GetByIdClient request, CancellationToken cancellationToken);
}