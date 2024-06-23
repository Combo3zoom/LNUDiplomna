namespace LightsOn.WebApp.Models.ServiceDescription;

[Serializable]
public class ServiceDescription(string headerText, string mainText, string lowerPriceLimit)
{
    public string HeaderText { get; set; } = headerText;
    public string MainText { get; set; } = mainText;
    public string LowerPriceLimit { get; set; } = lowerPriceLimit;

    private ServiceDescription() : this(string.Empty, string.Empty, string.Empty)
    {
    }
}