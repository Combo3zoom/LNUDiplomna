namespace LightsOn.Application.Client.Queries.GetByIdClient;

public class GetByIdClientQueryValidator : AbstractValidator<GetByIdClient>
{
    public GetByIdClientQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
    
}