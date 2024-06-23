using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Estimate.Queries.GetByIdEstimate;

namespace LightsOn.Application.Estimate.Queries.GetEstimates;

public record GetEstimates : IRequest<List<EstimateBriefDto>>;

public class GetEstimatesQueryHandler : IRequestHandler<GetEstimates, List<EstimateBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetEstimatesQueryHandlerStorageBroker _getEstimatesQueryHandlerStorageBroker;

    public GetEstimatesQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetEstimatesQueryHandlerStorageBroker getEstimatesQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getEstimatesQueryHandlerStorageBroker = getEstimatesQueryHandlerStorageBroker;
    }
    
    public async Task<List<EstimateBriefDto>> Handle(GetEstimates request, CancellationToken cancellationToken)
    {
        return await _getEstimatesQueryHandlerStorageBroker.GetEstimatesQueryHandler(request, cancellationToken);
    }
}

public class GetEstimatesQueryHandlerStorageBroker : IGetEstimatesQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEstimatesQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<EstimateBriefDto>> GetEstimatesQueryHandler(GetEstimates request, CancellationToken cancellationToken)
    {
        return await _context.Estimates
            .ProjectTo<EstimateBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetEstimatesQueryHandlerStorageBroker
{
    Task<List<EstimateBriefDto>> GetEstimatesQueryHandler(GetEstimates request, CancellationToken cancellationToken);
}