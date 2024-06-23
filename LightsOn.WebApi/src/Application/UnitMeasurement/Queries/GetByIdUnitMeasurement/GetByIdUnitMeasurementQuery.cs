using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

public record GetByIdUnitMeasurementQuery(int Id) : IRequest<UnitMeasurementBriefDto>;

public class GetByIdUnitMeasurementQueryHandler : IRequestHandler<GetByIdUnitMeasurementQuery, UnitMeasurementBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdUnitMeasurementQueryHandlerStorageBroker _getByIdUnitMeasurementQueryHandlerStorageBroker;

    public GetByIdUnitMeasurementQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdUnitMeasurementQueryHandlerStorageBroker getByIdUnitMeasurementQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdUnitMeasurementQueryHandlerStorageBroker = getByIdUnitMeasurementQueryHandlerStorageBroker;
    }

    public async Task<UnitMeasurementBriefDto> Handle(GetByIdUnitMeasurementQuery request, CancellationToken cancellationToken)
    {
        return await _getByIdUnitMeasurementQueryHandlerStorageBroker.GetByIdUnitMeasurement(request,
            cancellationToken);
    }
}

public class GetByIdUnitMeasurementQueryHandlerStorageBroker : IGetByIdUnitMeasurementQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdUnitMeasurementQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UnitMeasurementBriefDto> GetByIdUnitMeasurement(GetByIdUnitMeasurementQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.UnitMeasurements
            .Where(unitMeasurement => unitMeasurement.Id == request.Id)
            .ProjectTo<UnitMeasurementBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdUnitMeasurementQueryHandlerStorageBroker
{
    public Task<UnitMeasurementBriefDto> GetByIdUnitMeasurement(GetByIdUnitMeasurementQuery request,
        CancellationToken cancellationToken);
}