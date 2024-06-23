using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Estimate.Queries.GetByIdEstimate;

public record GetByIdEstimate(int Id) : IRequest<EstimateBriefDto>;

public class GetByIdEstimateQueryHandler : IRequestHandler<GetByIdEstimate, EstimateBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdEstimateQueryHandlerStorageBroker _getByIdEstimateQueryHandlerStorageBroker;

    public GetByIdEstimateQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdEstimateQueryHandlerStorageBroker getByIdEstimateQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdEstimateQueryHandlerStorageBroker = getByIdEstimateQueryHandlerStorageBroker;
    }

    public async Task<EstimateBriefDto> Handle(GetByIdEstimate request, CancellationToken cancellationToken)
    {
        return await _getByIdEstimateQueryHandlerStorageBroker.GetByIdEstimateQueryHandler(request, cancellationToken);
    }
}

public class GetByIdEstimateQueryHandlerStorageBroker : IGetByIdEstimateQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdEstimateQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EstimateBriefDto> GetByIdEstimateQueryHandler(GetByIdEstimate request, CancellationToken cancellationToken)
    {
        return await _context.Estimates.Where(estimate => estimate.Id == request.Id)
            .ProjectTo<EstimateBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdEstimateQueryHandlerStorageBroker
{
    Task<EstimateBriefDto> GetByIdEstimateQueryHandler(GetByIdEstimate request, CancellationToken cancellationToken);
}