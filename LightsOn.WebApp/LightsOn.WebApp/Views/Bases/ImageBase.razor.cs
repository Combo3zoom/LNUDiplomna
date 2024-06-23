using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Bases;

public partial class ImageBase : ComponentBase
{
    [Parameter]
    public string Src { get; set; }

    [Parameter]
    public string Alt { get; set; } = string.Empty;

    [Parameter]
    public string CssClass { get; set; } = string.Empty;
}