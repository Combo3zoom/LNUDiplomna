using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.WorkPerformanceDescription.Queries.GetByIdWorkPerformanceDescription;

public record GetByIdWorkPerformanceDescriptionQuery(int Id) : IRequest<WorkPerformanceDescriptionBriefDto>;

public class GetByIdWorkPerformanceDescriptionQueryHandler
    : IRequestHandler<GetByIdWorkPerformanceDescriptionQuery, WorkPerformanceDescriptionBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IGetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker
        _getByIdWorkPerformanceDescriptionQueryHandlerStorageBroker;

    public GetByIdWorkPerformanceDescriptionQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker getByIdWorkPerformanceDescriptionQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdWorkPerformanceDescriptionQueryHandlerStorageBroker = getByIdWorkPerformanceDescriptionQueryHandlerStorageBroker;
    }

    public async Task<WorkPerformanceDescriptionBriefDto> Handle(GetByIdWorkPerformanceDescriptionQuery request, CancellationToken cancellationToken)
    {
        return await _getByIdWorkPerformanceDescriptionQueryHandlerStorageBroker.GetByIdWorkPerformanceDescription(
            request, cancellationToken);
    }
}

public class GetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker
    : IGetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkPerformanceDescriptionBriefDto> GetByIdWorkPerformanceDescription(
        GetByIdWorkPerformanceDescriptionQuery request, CancellationToken cancellationToken)
    {
        return await _context.WorkPerformanceDescriptions
            .Where(w => w.Id == request.Id)
            .ProjectTo<WorkPerformanceDescriptionBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker
{
    public Task<WorkPerformanceDescriptionBriefDto> GetByIdWorkPerformanceDescription(
        GetByIdWorkPerformanceDescriptionQuery request, CancellationToken cancellationToken);
}