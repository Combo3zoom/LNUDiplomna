using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.CategoryExpense;
using LightsOn.Domain.Events.Client;

namespace LightsOn.Application.CategoryExpense.Commands.DeleteCategoryExpense;

public record DeleteCategoryExpenseCommand(int Id) : IRequest;

public class DeleteCategoryExpenseHandlerCommand : IRequestHandler<DeleteCategoryExpenseCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteCategoryExpenseCommandHandlerStorageBroker _deleteCategoryExpenseCommandHandlerStorageBroker;

    public DeleteCategoryExpenseHandlerCommand(IApplicationDbContext context,
        IDeleteCategoryExpenseCommandHandlerStorageBroker deleteCategoryExpenseCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteCategoryExpenseCommandHandlerStorageBroker = deleteCategoryExpenseCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        await _deleteCategoryExpenseCommandHandlerStorageBroker.DeleteCategoryExpense(request, cancellationToken);
    }
}

public class DeleteCategoryExpenseCommandHandlerStorageBroker : IDeleteCategoryExpenseCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryExpenseCommandHandlerStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DeleteCategoryExpense(DeleteCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _context.CategoryExpenses.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CategoryExpenses.Remove(entity);

        entity.AddDomainEvent(new CategoryExpenseDeleteEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteCategoryExpenseCommandHandlerStorageBroker
{
    Task DeleteCategoryExpense(DeleteCategoryExpenseCommand request, CancellationToken cancellationToken);
}