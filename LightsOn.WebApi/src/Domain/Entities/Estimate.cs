namespace LightsOn.Domain.Entities;

public class Estimate : BaseAuditableEntity
{
    public int CategoryExpenseId { get; set; }
    public CategoryExpense CategoryExpense  { get; set; }
    public int MaterialId { get; set; }
    public Material Material { get; set; }
    public uint MaterialsCount { get; set; }
    public uint UsedMaterialsCount { get; set; }

    public Estimate(CategoryExpense categoryExpense, Material material, uint materialsCount, uint usedMaterialsCount)
    {
        CategoryExpense = categoryExpense;
        Material = material;
        MaterialsCount = materialsCount;
        UsedMaterialsCount = usedMaterialsCount;
    }

    private Estimate()
    {
        CategoryExpense = null!;
        Material = null!;
    }
}