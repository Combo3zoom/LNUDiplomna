using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

namespace LightsOn.Application.UnitMeasurement.Queries.GetByNameUnitMeasurement;

public record GetByNameUnitMeasurementQuery(string Name) : IRequest<UnitMeasurementBriefDto>;

public class GetByNameUnitMeasurementQueryHandler : IRequestHandler<GetByNameUnitMeasurementQuery, UnitMeasurementBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByNameUnitMeasurementQueryHandlerStorageBroker _getByNameUnitMeasurementQueryHandlerStorageBroker;

    public GetByNameUnitMeasurementQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByNameUnitMeasurementQueryHandlerStorageBroker getByNameUnitMeasurementQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByNameUnitMeasurementQueryHandlerStorageBroker = getByNameUnitMeasurementQueryHandlerStorageBroker;
    }

    public async Task<UnitMeasurementBriefDto> Handle(GetByNameUnitMeasurementQuery request, CancellationToken cancellationToken)
    {
        return await _getByNameUnitMeasurementQueryHandlerStorageBroker.GetByIdUnitMeasurement(request,
            cancellationToken);
    }
}

public class GetByNameUnitMeasurementQueryHandlerStorageBroker : IGetByNameUnitMeasurementQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByNameUnitMeasurementQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UnitMeasurementBriefDto> GetByIdUnitMeasurement(GetByNameUnitMeasurementQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.UnitMeasurements
            .Where(unitMeasurement => unitMeasurement.Name == request.Name)
            .ProjectTo<UnitMeasurementBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByNameUnitMeasurementQueryHandlerStorageBroker
{
    public Task<UnitMeasurementBriefDto> GetByIdUnitMeasurement(GetByNameUnitMeasurementQuery request,
        CancellationToken cancellationToken);
}
