using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.ServiceDescription.Queries.GetServiceDescriptions;

public class GetServiceDescriptionsQuery : IRequest<List<ServiceDescriptionBriefDto>>;

public class GetServiceDescriptionsQueryHandler : IRequestHandler<GetServiceDescriptionsQuery, List<ServiceDescriptionBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetServiceDescriptionsQueryStorageBroker _getServiceDescriptionsQueryStorageBroker;

    public GetServiceDescriptionsQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetServiceDescriptionsQueryStorageBroker getServiceDescriptionsQueryStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getServiceDescriptionsQueryStorageBroker = getServiceDescriptionsQueryStorageBroker;
    }

    public async Task<List<ServiceDescriptionBriefDto>> Handle(GetServiceDescriptionsQuery request, CancellationToken cancellationToken)
    {
        return await _getServiceDescriptionsQueryStorageBroker.GetByIdServiceDescription(request, cancellationToken);
    }
}

public class GetServiceDescriptionsQueryStorageBroker : IGetServiceDescriptionsQueryStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetServiceDescriptionsQueryStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ServiceDescriptionBriefDto>> GetByIdServiceDescription(GetServiceDescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ServiceDescriptions
            .ProjectTo<ServiceDescriptionBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetServiceDescriptionsQueryStorageBroker
{
    public Task<List<ServiceDescriptionBriefDto>> GetByIdServiceDescription(GetServiceDescriptionsQuery request,
        CancellationToken cancellationToken);
}