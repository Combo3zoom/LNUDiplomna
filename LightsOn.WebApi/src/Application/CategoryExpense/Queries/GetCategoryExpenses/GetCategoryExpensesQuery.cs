using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;
using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.CategoryExpense.Queries.GetCategoryExpenses;

public record GetCategoryExpensesQuery() : IRequest<List<CategoryExpenseBriefDto>>;

public class GetCategoryExpensesQueryHandler : IRequestHandler<GetCategoryExpensesQuery, List<CategoryExpenseBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetCategoryExpensesQueryHandlerStorageBroker _getCategoryExpensesQueryHandlerStorageBroker;

    public GetCategoryExpensesQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetCategoryExpensesQueryHandlerStorageBroker getCategoryExpensesQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getCategoryExpensesQueryHandlerStorageBroker = getCategoryExpensesQueryHandlerStorageBroker;
    }

    public async Task<List<CategoryExpenseBriefDto>> Handle(GetCategoryExpensesQuery request, CancellationToken cancellationToken)
    {
        return await _getCategoryExpensesQueryHandlerStorageBroker.GetCategoryExpenseHandle(request, cancellationToken);
    }
}

public class GetCategoryExpensesQueryHandlerStorageBroker : IGetCategoryExpensesQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryExpensesQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryExpenseBriefDto>> GetCategoryExpenseHandle(GetCategoryExpensesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CategoryExpenses
            .ProjectTo<CategoryExpenseBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public interface IGetCategoryExpensesQueryHandlerStorageBroker
{
    Task<List<CategoryExpenseBriefDto>> GetCategoryExpenseHandle(GetCategoryExpensesQuery request,
        CancellationToken cancellationToken);
}