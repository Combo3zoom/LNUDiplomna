using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Application.CompanyPhoneNumber.Commands.CreateCompanyPhoneNumber;

public record CreateCompanyPhoneNumberCommand(string PhoneNumber) : IRequest<int>;

public class CreateCompanyPhoneNumberCommandHandler : IRequestHandler<CreateCompanyPhoneNumberCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateCompanyPhoneNumberCommandHandlerStorageBroker _createCompanyPhoneNumberHandlerStorageBroker;

    public CreateCompanyPhoneNumberCommandHandler(IApplicationDbContext context,
        ICreateCompanyPhoneNumberCommandHandlerStorageBroker createCompanyPhoneNumberHandlerStorageBroker)
    {
        _context = context;
        _createCompanyPhoneNumberHandlerStorageBroker = createCompanyPhoneNumberHandlerStorageBroker;
    }

    public async Task<int> Handle(CreateCompanyPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        return await _createCompanyPhoneNumberHandlerStorageBroker.CreateCompanyPhoneNumber(request, cancellationToken);
    }
}

public class CreateCompanyPhoneNumberCommandHandlerStorageBroker : ICreateCompanyPhoneNumberCommandHandlerStorageBroker
{
    private readonly IApplicationDbContext _context;

    public CreateCompanyPhoneNumberCommandHandlerStorageBroker(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCompanyPhoneNumber(CreateCompanyPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.CompanyPhoneNumber(request.PhoneNumber); 

        _context.CompanyPhoneNumbers.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public interface ICreateCompanyPhoneNumberCommandHandlerStorageBroker
{
    Task<int> CreateCompanyPhoneNumber(CreateCompanyPhoneNumberCommand request, CancellationToken cancellationToken);
}
