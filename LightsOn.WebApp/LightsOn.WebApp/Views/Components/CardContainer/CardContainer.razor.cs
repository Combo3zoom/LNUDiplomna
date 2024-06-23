using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.CardContainer;

public partial class CardContainer : ComponentBase
{
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }

    public bool DetailsVisible { get; set; }
    private string DetailsHiddenClass => DetailsVisible ? "" : "hidden";
    
    private Task OnButtonPressed()
    {
        DetailsVisible = !DetailsVisible;
        
        StateHasChanged();
        
        return Task.CompletedTask;
    }
}