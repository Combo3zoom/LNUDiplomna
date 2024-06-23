using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Engine.Queries.GetByIdEngine;

public record GetByIdEngine(int Id) : IRequest<EngineBriefDto>;

public class GetByIdEngineHandler : IRequestHandler<GetByIdEngine, EngineBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdEngineHandlerStorageBroker _getByIdEngineHandlerStorageBroker;

    public GetByIdEngineHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdEngineHandlerStorageBroker getByIdEngineHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdEngineHandlerStorageBroker = getByIdEngineHandlerStorageBroker;
    }

    public async Task<EngineBriefDto> Handle(GetByIdEngine request, CancellationToken cancellationToken)
    {
        return await _getByIdEngineHandlerStorageBroker.GetByIdEngineHandler(request, cancellationToken);
    }
}

public class GetByIdEngineHandlerStorageBroker : IGetByIdEngineHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdEngineHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EngineBriefDto> GetByIdEngineHandler(GetByIdEngine request, CancellationToken cancellationToken)
    {
        return await _context.Engines
            .Where(engine => engine.Id == request.Id)
            .ProjectTo<EngineBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdEngineHandlerStorageBroker
{
    Task<EngineBriefDto> GetByIdEngineHandler(GetByIdEngine request, CancellationToken cancellationToken);
}