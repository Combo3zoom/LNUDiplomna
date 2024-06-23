using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Bases;

public partial class LabelBase : ComponentBase
{
    [Parameter]
    public required string Label { get; set; }
    
    [Parameter]
    public string CssClass { get; set; } = string.Empty;
    
    private string GetCssClass() => string.IsNullOrEmpty(CssClass) ? "span" : CssClass;
    
}