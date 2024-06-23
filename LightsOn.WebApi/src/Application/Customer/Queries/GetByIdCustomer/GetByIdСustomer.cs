using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.Customer.Queries.GetByIdCustomer;

public record GetByIdCustomer(int Id) : IRequest<CustomerBriefDto>;

public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomer, CustomerBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdCustomerStorageBroker _getByIdCustomerStorageBroker;

    public GetByIdCustomerHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdCustomerStorageBroker getByIdCustomerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdCustomerStorageBroker = getByIdCustomerStorageBroker;
    }

    public Task<CustomerBriefDto> Handle(GetByIdCustomer request, CancellationToken cancellationToken)
    {
        var customerBriefDto = _getByIdCustomerStorageBroker.GetByIdCustomer(request, cancellationToken);

        if (customerBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(_context.Clients));

        return Task.FromResult(customerBriefDto);
    }
}

public class GetByIdCustomerStorageBroker : IGetByIdCustomerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdCustomerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public CustomerBriefDto? GetByIdCustomer(GetByIdCustomer request, CancellationToken cancellationToken)
    {
        var customerBriefDto = _context.Customers
            .Where(client => client.Id == request.Id);
        
        var a = customerBriefDto.ProjectTo<CustomerBriefDto>(_mapper.ConfigurationProvider);
        
        return a.SingleOrDefault();
    }
}

public interface IGetByIdCustomerStorageBroker
{
    public CustomerBriefDto? GetByIdCustomer(GetByIdCustomer request, CancellationToken cancellationToken);
}