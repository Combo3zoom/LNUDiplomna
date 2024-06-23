using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.CompanyPhoneNumber.Commands.DeleteCompanyPhoneNumber;

public record DeleteCompanyPhoneNumberCommand(int Id) : IRequest;

public class DeleteCompanyPhoneNumberCommandHandler : IRequestHandler<DeleteCompanyPhoneNumberCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDeleteCompanyPhoneNumberCommandHandlerStorageBroker _deleteCompanyPhoneNumberCommandHandlerStorageBroker;

    public DeleteCompanyPhoneNumberCommandHandler(IApplicationDbContext context,
        IDeleteCompanyPhoneNumberCommandHandlerStorageBroker deleteCompanyPhoneNumberCommandHandlerStorageBroker)
    {
        _context = context;
        _deleteCompanyPhoneNumberCommandHandlerStorageBroker = deleteCompanyPhoneNumberCommandHandlerStorageBroker;
    }

    public async Task Handle(DeleteCompanyPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        await _deleteCompanyPhoneNumberCommandHandlerStorageBroker.DeleteCompanyPhoneNumber(request, cancellationToken);
    }
}

public class DeleteCompanyPhoneNumberCommandHandlerStorageBroker : IDeleteCompanyPhoneNumberCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public DeleteCompanyPhoneNumberCommandHandlerStorageBroker(IApplicationDbContext context) => _context = context;
    
    public async Task DeleteCompanyPhoneNumber(DeleteCompanyPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyPhoneNumbers
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        _context.CompanyPhoneNumbers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IDeleteCompanyPhoneNumberCommandHandlerStorageBroker
{
    Task DeleteCompanyPhoneNumber(DeleteCompanyPhoneNumberCommand request, CancellationToken cancellationToken);
}