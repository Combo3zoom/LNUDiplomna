using LightsOn.WebApp.Brokers.Navigations;

namespace LightsOn.WebApp.Services.Views.SidebarView;

public class SidebarViewService : ISidebarViewService
{
    private readonly INavigationBroker _navigationBroker;

    public SidebarViewService(INavigationBroker navigationBroker)
    {
        _navigationBroker = navigationBroker;
    }

    public void NavigateToHome()
    {
        _navigationBroker.NavigateToHome();
    }

    public void NavigateToOffers()
    {
        _navigationBroker.NavigateToOffers();
    }

    public void NavigateToClients()
    {
        _navigationBroker.NavigateToClients();
    }

    public void NavigateToSignIn()
    {
        _navigationBroker.NavigateToSignIn();
    }

    public void NavigateToSignOut()
    {
        _navigationBroker.NavigateToSignOut();
    }
}