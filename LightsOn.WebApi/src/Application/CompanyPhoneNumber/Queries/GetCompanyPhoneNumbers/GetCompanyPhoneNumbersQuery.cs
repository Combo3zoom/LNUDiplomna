using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.CompanyPhoneNumber.Queries.GetCompanyPhoneNumbers;

public record GetCompanyPhoneNumbersQuery : IRequest<List<CompanyPhoneNumberBriefDto>>;

public class GetCompanyPhoneNumbersQueryHandler : IRequestHandler<GetCompanyPhoneNumbersQuery,
    List<CompanyPhoneNumberBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetCompanyPhoneNumbersQueryStorageBroker _getCompanyPhoneNumbersQueryStorageBroker;

    public GetCompanyPhoneNumbersQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetCompanyPhoneNumbersQueryStorageBroker getCompanyPhoneNumbersQueryStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getCompanyPhoneNumbersQueryStorageBroker = getCompanyPhoneNumbersQueryStorageBroker;
    }

    public async Task<List<CompanyPhoneNumberBriefDto>> Handle(GetCompanyPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        return await _getCompanyPhoneNumbersQueryStorageBroker.GetByIdCompanyPhoneNumber(request, cancellationToken);
    }
}

public class GetCompanyPhoneNumbersQueryStorageBroker : IGetCompanyPhoneNumbersQueryStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyPhoneNumbersQueryStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<CompanyPhoneNumberBriefDto>> GetByIdCompanyPhoneNumber(GetCompanyPhoneNumbersQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CompanyPhoneNumbers
            .ProjectTo<CompanyPhoneNumberBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetCompanyPhoneNumbersQueryStorageBroker
{
    public Task<List<CompanyPhoneNumberBriefDto>> GetByIdCompanyPhoneNumber(
        GetCompanyPhoneNumbersQuery request, CancellationToken cancellationToken);
}