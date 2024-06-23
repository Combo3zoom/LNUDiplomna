using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.Navigate;

public partial class NavigateToHome : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        NavigationManager.NavigateTo("/");
    }
}