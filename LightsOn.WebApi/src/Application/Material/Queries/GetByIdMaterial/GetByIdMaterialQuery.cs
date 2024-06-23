using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Material.Queries.GetByIdMaterial;

public record GetByIdMaterialQuery(int Id) : IRequest<MaterialBriefDto>;

public class GetByIdMaterialQueryHandler : IRequestHandler<GetByIdMaterialQuery, MaterialBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdMaterialQueryHandlerStorageBroker _getByIdMaterialQueryHandlerStorageBroker;

    public GetByIdMaterialQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdMaterialQueryHandlerStorageBroker getByIdMaterialQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdMaterialQueryHandlerStorageBroker = getByIdMaterialQueryHandlerStorageBroker;
    }

    public async Task<MaterialBriefDto> Handle(GetByIdMaterialQuery request, CancellationToken cancellationToken)
    {
        return await _getByIdMaterialQueryHandlerStorageBroker.GetByIdMaterial(request, cancellationToken);
    }
}

public class GetByIdMaterialQueryHandlerStorageBroker : IGetByIdMaterialQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdMaterialQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MaterialBriefDto> GetByIdMaterial(GetByIdMaterialQuery request, CancellationToken cancellationToken)
    {
        return await _context.Materials
            .Where(material => material.Id == request.Id)
            .ProjectTo<MaterialBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdMaterialQueryHandlerStorageBroker
{
    public Task<MaterialBriefDto> GetByIdMaterial(GetByIdMaterialQuery request, CancellationToken cancellationToken);
}