using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Customer.Queries.GetByIdCustomer;

namespace LightsOn.Application.Customer.Queries.GetCustomers;


public record GetCustomersQuery : IRequest<List<CustomerBriefDto>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetCustomersQueryStorageBroker _getCustomersQueryStorageBroker;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetCustomersQueryStorageBroker getCustomersQueryStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getCustomersQueryStorageBroker = getCustomersQueryStorageBroker;
    }

    public async Task<List<CustomerBriefDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _getCustomersQueryStorageBroker.GetByIdClient(request, cancellationToken);
    }
}

public class GetCustomersQueryStorageBroker : IGetCustomersQueryStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<CustomerBriefDto>> GetByIdClient(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .ProjectTo<CustomerBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetCustomersQueryStorageBroker
{
    public Task<List<CustomerBriefDto>> GetByIdClient(GetCustomersQuery request, CancellationToken cancellationToken);
}