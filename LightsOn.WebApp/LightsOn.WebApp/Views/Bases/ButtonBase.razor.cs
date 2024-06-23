using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Bases;

public partial class ButtonBase : ComponentBase
{
    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public EventCallback  OnButtonPressed { get; set; }

    [Parameter] 
    public bool IsDisabled { get; set; }
    
    [Parameter]
    public string CssClass { get; set; } = string.Empty;
    
    [Parameter]
    public string IconCss { get; set; } = string.Empty;
    
    [Parameter]
    public bool IsToggle { get; set; }
    

    public async Task OnClick()
    {
        if (OnButtonPressed.HasDelegate)
        {
            await OnButtonPressed.InvokeAsync(null);
        }
    }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    public void Disable()
    {
        IsDisabled = true;
        InvokeAsync(StateHasChanged);
    }
    
    public void Enable()
    {
        IsDisabled = false;
        InvokeAsync(StateHasChanged);
    }
}