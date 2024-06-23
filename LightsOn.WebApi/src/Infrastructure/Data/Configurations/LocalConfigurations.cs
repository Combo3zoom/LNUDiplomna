namespace LightsOn.Infrastructure.Data.Configurations;

public class LocalConfigurations
{
    public ApiConfigurations ApiConfigurations { get; set; }

    public LocalConfigurations(ApiConfigurations apiConfigurations)
    {
        this.ApiConfigurations = apiConfigurations;
    }
    private LocalConfigurations()
    {
        this.ApiConfigurations = null!;
    }
}