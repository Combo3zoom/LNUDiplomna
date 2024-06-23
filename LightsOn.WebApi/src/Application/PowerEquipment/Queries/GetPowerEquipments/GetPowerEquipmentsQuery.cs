using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

namespace LightsOn.Application.PowerEquipment.Queries.GetPowerEquipments;

public record GetPowerEquipmentsQuery() : IRequest<List<PowerEquipmentBriefDto>>;

public class GetPowerEquipmentsQueryHandler : IRequestHandler<GetPowerEquipmentsQuery, List<PowerEquipmentBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetPowerEquipmentsQueryHandlerStorageBroker _getPowerEquipmentsQueryHandlerStorageBroker;

    public GetPowerEquipmentsQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetPowerEquipmentsQueryHandlerStorageBroker getPowerEquipmentsQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getPowerEquipmentsQueryHandlerStorageBroker = getPowerEquipmentsQueryHandlerStorageBroker;
    }

    public async Task<List<PowerEquipmentBriefDto>> Handle(GetPowerEquipmentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _getPowerEquipmentsQueryHandlerStorageBroker.GetPowerEquipments(request, cancellationToken);
    }
}

public class GetPowerEquipmentsQueryHandlerStorageBroker : IGetPowerEquipmentsQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPowerEquipmentsQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PowerEquipmentBriefDto>> GetPowerEquipments(GetPowerEquipmentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.PowerEquipments
            .ProjectTo<PowerEquipmentBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }   
}

public interface IGetPowerEquipmentsQueryHandlerStorageBroker
{
    public Task<List<PowerEquipmentBriefDto>> GetPowerEquipments(GetPowerEquipmentsQuery request,
        CancellationToken cancellationToken);
}