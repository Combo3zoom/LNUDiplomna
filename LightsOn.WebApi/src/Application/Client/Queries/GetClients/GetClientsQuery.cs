using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Client.Queries.GetClients;

public record GetClientsQuery : IRequest<List<ClientBriefDto>>;

public class GetCompanyPhoneNumbersQueryHandler : IRequestHandler<GetClientsQuery, List<ClientBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetClientsQueryStorageBroker _getClientsQueryStorageBroker;

    public GetCompanyPhoneNumbersQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetClientsQueryStorageBroker getClientsQueryStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getClientsQueryStorageBroker = getClientsQueryStorageBroker;
    }

    public async Task<List<ClientBriefDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _getClientsQueryStorageBroker.GetByIdClient(request, cancellationToken);
    }
}

public class GetClientsQueryStorageBroker : IGetClientsQueryStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetClientsQueryStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ClientBriefDto>> GetByIdClient(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Clients
            .ProjectTo<ClientBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetClientsQueryStorageBroker
{
    public Task<List<ClientBriefDto>> GetByIdClient(GetClientsQuery request, CancellationToken cancellationToken);
}