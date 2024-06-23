using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

namespace LightsOn.Application.UnitMeasurement.Queries.GetUnitMeasurements;

public record GetUnitMeasurementsQuery() : IRequest<List<UnitMeasurementBriefDto>>;

public class GetUnitMeasurementsQueryHandler : IRequestHandler<GetUnitMeasurementsQuery, List<UnitMeasurementBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetUnitMeasurementsQueryHandlerStorageBroker _getUnitMeasurementsQueryHandlerStorageBroker;

    public GetUnitMeasurementsQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetUnitMeasurementsQueryHandlerStorageBroker getUnitMeasurementsQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getUnitMeasurementsQueryHandlerStorageBroker = getUnitMeasurementsQueryHandlerStorageBroker;
    }

    public async Task<List<UnitMeasurementBriefDto>> Handle(GetUnitMeasurementsQuery request, CancellationToken cancellationToken)
    {
        return await _getUnitMeasurementsQueryHandlerStorageBroker.GetUnitMeasurements(request, cancellationToken);
    }
}

public class GetUnitMeasurementsQueryHandlerStorageBroker : IGetUnitMeasurementsQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUnitMeasurementsQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UnitMeasurementBriefDto>> GetUnitMeasurements(GetUnitMeasurementsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.UnitMeasurements
            .ProjectTo<UnitMeasurementBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetUnitMeasurementsQueryHandlerStorageBroker
{
    public Task<List<UnitMeasurementBriefDto>> GetUnitMeasurements(GetUnitMeasurementsQuery request,
        CancellationToken cancellationToken);
}
