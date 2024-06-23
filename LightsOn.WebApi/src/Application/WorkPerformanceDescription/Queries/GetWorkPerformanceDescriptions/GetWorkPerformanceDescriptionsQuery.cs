using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.WorkPerformanceDescription.Queries.GetByIdWorkPerformanceDescription;

namespace LightsOn.Application.WorkPerformanceDescription.Queries.GetWorkPerformanceDescriptions;

public record GetWorkPerformanceDescriptionsQuery() : IRequest<List<WorkPerformanceDescriptionBriefDto>>;

public class GetWorkPerformanceDescriptionsQueryHandler 
    : IRequestHandler<GetWorkPerformanceDescriptionsQuery, List<WorkPerformanceDescriptionBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IGetWorkPerformanceDescriptionsQueryHandlerStorageBroker
        _getWorkPerformanceDescriptionsQueryHandlerStorageBroker;

    public GetWorkPerformanceDescriptionsQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetWorkPerformanceDescriptionsQueryHandlerStorageBroker getWorkPerformanceDescriptionsQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getWorkPerformanceDescriptionsQueryHandlerStorageBroker = getWorkPerformanceDescriptionsQueryHandlerStorageBroker;
    }

    public async Task<List<WorkPerformanceDescriptionBriefDto>> Handle(GetWorkPerformanceDescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _getWorkPerformanceDescriptionsQueryHandlerStorageBroker.GetWorkPerformanceDescriptions(request,
            cancellationToken);
    }
}

public class GetWorkPerformanceDescriptionsQueryHandlerStorageBroker : IGetWorkPerformanceDescriptionsQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkPerformanceDescriptionsQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkPerformanceDescriptionBriefDto>> GetWorkPerformanceDescriptions(GetWorkPerformanceDescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkPerformanceDescriptions
            .ProjectTo<WorkPerformanceDescriptionBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetWorkPerformanceDescriptionsQueryHandlerStorageBroker
{
    public Task<List<WorkPerformanceDescriptionBriefDto>> GetWorkPerformanceDescriptions(
        GetWorkPerformanceDescriptionsQuery request,
        CancellationToken cancellationToken);
}