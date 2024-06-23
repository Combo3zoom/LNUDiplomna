using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Material.Queries.GetByIdMaterial;

namespace LightsOn.Application.Material.Queries.GetMaterials;

public record GetMaterialsQuery() : IRequest<List<MaterialBriefDto>>;

public class GetMaterialQueryHandler : IRequestHandler<GetMaterialsQuery, List<MaterialBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetMaterialQueryHandlerStorageBroker _getMaterialQueryHandlerStorageBroker;

    public GetMaterialQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetMaterialQueryHandlerStorageBroker getMaterialQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getMaterialQueryHandlerStorageBroker = getMaterialQueryHandlerStorageBroker;
    }

    public async Task<List<MaterialBriefDto>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        return await _getMaterialQueryHandlerStorageBroker.GetMaterial(request, cancellationToken);
    }
}

public class GetMaterialQueryHandlerStorageBroker : IGetMaterialQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMaterialQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MaterialBriefDto>> GetMaterial(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Materials
            .ProjectTo<MaterialBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetMaterialQueryHandlerStorageBroker
{
    public Task<List<MaterialBriefDto>> GetMaterial(GetMaterialsQuery request,
        CancellationToken cancellationToken);
}