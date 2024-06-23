using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.CategoryExpense;

namespace LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;

public record CreateCategoryExpenseCommand(string Name) : IRequest<int>;

public class CreateCategoryExpenseCommandHandler : IRequestHandler<CreateCategoryExpenseCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateCategoryExpenseCommandHandlerStorageBroker _createCategoryExpenseCommandHandlerStorageBroker;

    public CreateCategoryExpenseCommandHandler(IApplicationDbContext context,
        ICreateCategoryExpenseCommandHandlerStorageBroker createCategoryExpenseCommandHandlerStorageBroker)
    {
        _context = context;
        _createCategoryExpenseCommandHandlerStorageBroker = createCategoryExpenseCommandHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        return await _createCategoryExpenseCommandHandlerStorageBroker.CreateCategory(request, cancellationToken);
    }
}

public class CreateCategoryExpenseCommandHandlerStorageBroker : ICreateCategoryExpenseCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryExpenseCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task<int> CreateCategory(CreateCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.CategoryExpense(request.Name);

        entity.AddDomainEvent(new CategoryExpenseCreatedEvent(entity));

        _context.CategoryExpenses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateCategoryExpenseCommandHandlerStorageBroker
{
    Task<int> CreateCategory(CreateCategoryExpenseCommand request, CancellationToken cancellationToken);
}