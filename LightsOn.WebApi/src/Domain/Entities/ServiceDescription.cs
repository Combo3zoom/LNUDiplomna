namespace LightsOn.Domain.Entities;

public class ServiceDescription(string headerText, string mainText, string lowerPriceLimit) : BaseEntity
{
    public string HeaderText { get; set; } = headerText;
    public string MainText { get; set; } = mainText;
    public string LowerPriceLimit { get; set; } = lowerPriceLimit;

    private ServiceDescription() : this(string.Empty, string.Empty, string.Empty)
    {
    }
}