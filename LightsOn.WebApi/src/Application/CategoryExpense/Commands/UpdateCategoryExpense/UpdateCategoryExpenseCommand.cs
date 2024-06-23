using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;

public record UpdateCategoryExpenseCommand(int Id, string Name) : IRequest;

public class UpdateCategoryExpenseCommandHandler : IRequestHandler<UpdateCategoryExpenseCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUpdateCategoryExpenseCommandHandlerStorageBroker _updateCategoryExpenseCommandHandlerStorageBroker;

    public UpdateCategoryExpenseCommandHandler(IApplicationDbContext context,
        IUpdateCategoryExpenseCommandHandlerStorageBroker updateCategoryExpenseCommandHandlerStorageBroker)
    {
        _context = context;
        _updateCategoryExpenseCommandHandlerStorageBroker = updateCategoryExpenseCommandHandlerStorageBroker;
    }

    public async Task Handle(UpdateCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        await _updateCategoryExpenseCommandHandlerStorageBroker.UpdateCategoryExpense(request, cancellationToken);
    }
}

public class UpdateCategoryExpenseCommandHandlerStorageBroker : IUpdateCategoryExpenseCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryExpenseCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;

    public async Task UpdateCategoryExpense(UpdateCategoryExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryExpenses
            .FindAsync(new object?[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUpdateCategoryExpenseCommandHandlerStorageBroker
{
    Task UpdateCategoryExpense(UpdateCategoryExpenseCommand request, CancellationToken cancellationToken);
}