namespace LightsOn.Application.WorkPerformanceDescription.Queries.GetByIdWorkPerformanceDescription;

public class GetByIdWorkPerformanceDescriptionQueryValidation : AbstractValidator<GetByIdWorkPerformanceDescriptionQuery>
{
    public GetByIdWorkPerformanceDescriptionQueryValidation()
    {
        RuleFor(w => w.Id)
            .NotEmpty();
    }
}