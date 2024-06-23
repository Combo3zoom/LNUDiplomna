using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Brokers.Navigations;

public class NavigationBroker : INavigationBroker
{
    private readonly NavigationManager _navigationManager;

    public NavigationBroker(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;
    
    public void NavigateToHome() => _navigationManager.NavigateTo("/home");
    public void NavigateToOffers() => _navigationManager.NavigateTo("/offers");
    public void NavigateToClients() => _navigationManager.NavigateTo("/clients");
    public void NavigateToSignIn() => _navigationManager.NavigateTo("MicrosoftIdentity/Account/SignIn", 
        forceLoad: true);
    public void NavigateToSignOut() => _navigationManager.NavigateTo("MicrosoftIdentity/Account/SignOut", 
        forceLoad: true);
}