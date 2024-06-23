namespace LightsOn.Infrastructure.Data.Configurations;

public class ApiConfigurations
{
    public string Url { get; set; }

    public ApiConfigurations(string url)
    {
        Url = url;
    }

    private ApiConfigurations()
    {
        Url = string.Empty;
    }
}