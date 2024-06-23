using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;

public record GetByIdCategoryExpenseQuery(int Id) : IRequest<CategoryExpenseBriefDto>;

public class GetByIdCategoryExpenseQueryHandler : IRequestHandler<GetByIdCategoryExpenseQuery, CategoryExpenseBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGetByIdCategoryExpenseQueryHandlerStorageBroker _getByIdCategoryExpenseQueryHandlerStorageBroker;

    public GetByIdCategoryExpenseQueryHandler(IApplicationDbContext context, IMapper mapper,
        IGetByIdCategoryExpenseQueryHandlerStorageBroker getByIdCategoryExpenseQueryHandlerStorageBroker)
    {
        _context = context;
        _mapper = mapper;
        _getByIdCategoryExpenseQueryHandlerStorageBroker = getByIdCategoryExpenseQueryHandlerStorageBroker;
    }

    public async Task<CategoryExpenseBriefDto> Handle(GetByIdCategoryExpenseQuery request, CancellationToken cancellationToken)
    {
        return await _getByIdCategoryExpenseQueryHandlerStorageBroker.GetByIdCategoryExpense(request, cancellationToken);
    }
}

public class GetByIdCategoryExpenseQueryHandlerStorageBroker : IGetByIdCategoryExpenseQueryHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdCategoryExpenseQueryHandlerStorageBroker(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryExpenseBriefDto> GetByIdCategoryExpense(GetByIdCategoryExpenseQuery request, CancellationToken cancellationToken)
    {
        return await _context.CategoryExpenses
            .Where(categoryExpense => categoryExpense.Id == request.Id)
            .ProjectTo<CategoryExpenseBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}

public interface IGetByIdCategoryExpenseQueryHandlerStorageBroker
{
    public Task<CategoryExpenseBriefDto> GetByIdCategoryExpense(GetByIdCategoryExpenseQuery request, CancellationToken cancellationToken);
}