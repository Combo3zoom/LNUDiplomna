using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Engine.Queries.GetByIdEngine;

namespace LightsOn.Application.Engine.Queries.GetEngines;

public record GetEngines : IRequest<List<EngineBriefDto>>;

public class GetEnginesHandlerQuery : IRequestHandler<GetEngines, List<EngineBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetEnginesHandlerQueryStorageBroker _getEnginesHandlerQueryStorageBroker;

    public GetEnginesHandlerQuery(IApplicationDbContext context, IMapper mapper,
        IGetEnginesHandlerQueryStorageBroker getEnginesHandlerQueryStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getEnginesHandlerQueryStorageBroker = getEnginesHandlerQueryStorageBroker;
    }

    public async Task<List<EngineBriefDto>> Handle(GetEngines request, CancellationToken cancellationToken)
    {
        return await _getEnginesHandlerQueryStorageBroker.GetEnginesHandlerQuery(request, cancellationToken);
    }
}

public class GetEnginesHandlerQueryStorageBroker : IGetEnginesHandlerQueryStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEnginesHandlerQueryStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EngineBriefDto>> GetEnginesHandlerQuery(GetEngines request, CancellationToken cancellationToken)
    {
        return await _context.Engines
            .ProjectTo<EngineBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetEnginesHandlerQueryStorageBroker
{
    Task<List<EngineBriefDto>> GetEnginesHandlerQuery(GetEngines request, CancellationToken cancellationToken);
}