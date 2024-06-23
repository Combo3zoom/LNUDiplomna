using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Material.Queries.GetByIdMaterial;

namespace LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;

public record GetByIdPowerEquipmentQuery(int Id) : IRequest<PowerEquipmentBriefDto>;

public class GetByIdPowerEquipmentQueryHandler : IRequestHandler<GetByIdPowerEquipmentQuery, PowerEquipmentBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdPowerEquipmentQueryHandlerStorageBroker _getByIdPowerEquipmentQueryHandlerStorageBroker;

    public GetByIdPowerEquipmentQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdPowerEquipmentQueryHandlerStorageBroker getByIdPowerEquipmentQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdPowerEquipmentQueryHandlerStorageBroker = getByIdPowerEquipmentQueryHandlerStorageBroker;
    }

    public async Task<PowerEquipmentBriefDto> Handle(GetByIdPowerEquipmentQuery request, CancellationToken cancellationToken)
    {
        return await _getByIdPowerEquipmentQueryHandlerStorageBroker.GetByIdPowerEquipment(request, cancellationToken);
    }
}

public class GetByIdPowerEquipmentQueryHandlerStorageBroker : IGetByIdPowerEquipmentQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdPowerEquipmentQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PowerEquipmentBriefDto> GetByIdPowerEquipment(GetByIdPowerEquipmentQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.PowerEquipments
            .Where(powerEquipment => powerEquipment.Id == request.Id)
            .ProjectTo<PowerEquipmentBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdPowerEquipmentQueryHandlerStorageBroker
{
    public Task<PowerEquipmentBriefDto> GetByIdPowerEquipment(GetByIdPowerEquipmentQuery request,
        CancellationToken cancellationToken);
}