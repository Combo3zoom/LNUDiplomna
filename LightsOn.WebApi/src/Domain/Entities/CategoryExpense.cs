namespace LightsOn.Domain.Entities;

public class CategoryExpense : BaseAuditableEntity
{
    public string Name { get; set; }
    public int EstimateId { get; set; }
    public IList<Estimate> Estimates { get; private set; }

    public CategoryExpense(string name)
    {
        Name = name;
        Estimates = new List<Estimate>();
    }
    
    private CategoryExpense()
    {
        Name = string.Empty;
        Estimates = new List<Estimate>();
    }
}