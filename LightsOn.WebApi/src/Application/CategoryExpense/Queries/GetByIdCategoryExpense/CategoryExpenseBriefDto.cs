using LightsOn.Domain.Entities;

namespace LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;

public record CategoryExpenseBriefDto
{
    public string Name { get; init; }
    public IList<Domain.Entities.Estimate> Estimates { get; init; }

    private CategoryExpenseBriefDto()
    {
        Name = string.Empty;
        Estimates = null!;
    }
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.CategoryExpense, CategoryExpenseBriefDto>();
        
    }
}