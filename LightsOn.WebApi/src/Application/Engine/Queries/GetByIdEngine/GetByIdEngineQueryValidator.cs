using System.Data;

namespace LightsOn.Application.Engine.Queries.GetByIdEngine;

public class GetByIdEngineQueryValidator : AbstractValidator<GetByIdEngine>
{
    public GetByIdEngineQueryValidator()
    {
        RuleFor(engine => engine.Id)
            .NotEmpty();
    }
}