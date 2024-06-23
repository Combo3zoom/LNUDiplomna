using LightsOn.Domain.Entities;

namespace LightsOn.Application.Estimate.Queries.GetByIdEstimate;

public record EstimateBriefDto
{
    public Domain.Entities.CategoryExpense CategoryExpense  { get; init; }
    public Domain.Entities.Material Material { get; init; }
    public uint MaterialsCount { get; init; }
    public uint UsedMaterialsCount { get; init; }
    
    private EstimateBriefDto()
    {
        CategoryExpense = null!;
        Material = null!;
    }

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Domain.Entities.Estimate, EstimateBriefDto>();
    }
}