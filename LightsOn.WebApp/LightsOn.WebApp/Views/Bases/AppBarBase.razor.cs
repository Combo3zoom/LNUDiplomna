using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace LightsOn.WebApp.Views.Bases;

public partial class AppBarBase
{
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }
    
    public async Task OnMainButtonPressed()
    {
        NavigationManager.NavigateTo("/");
    }
    
    public async Task OnServicesButtonPressed()
    {
        NavigationManager.NavigateTo("/Services");
    }
}