using System.Collections.Immutable;
using LightsOn.WebApp.Services.Views.SidebarView;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Navigations;

namespace LightsOn.WebApp.Views.Bases;

public delegate void OnClickEventHandler();

public sealed record SidebarMenu(
    ImmutableList<SidebarMenuItem> SidebarMenuItems);
public sealed record SidebarMenuItem(string IconSubClass, string Title, OnClickEventHandler OnClickEventHandler);

public partial class SidebarBase
{
    public SidebarBase()
    {
        var sidebarMenuItems = ImmutableList.Create(
            items: new[]
            {
                new SidebarMenuItem("home", "Головна", OnHomeItemClicked),
                new SidebarMenuItem("home", "Контакти", OnHomeItemClicked),
                new SidebarMenuItem("home", "Послуги", OnHomeItemClicked)
            });

        SidebarMenu = new SidebarMenu(sidebarMenuItems);
    }

    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    [CascadingParameter]
    public Task<AuthenticationState>? AuthenticationStateTask { get; set; }
    
    [Inject] 
    public required ISidebarViewService SidebarViewService { get; set; }

    public SidebarMenu SidebarMenu { get; set; }

    public SfSidebar SfSidebar { get; set; }
    
    public bool IsAuthorized { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationStateTask == null)
        {
            await base.OnInitializedAsync();
            return;
        }
            
        var authenticationState = await AuthenticationStateTask;
        IsAuthorized = authenticationState.User.Identity?.IsAuthenticated ?? false;

        if (!IsAuthorized)
        {
            await base.OnInitializedAsync();
            return;
        }
        
        var items = SidebarMenu.SidebarMenuItems.AddRange(new[]
        {
            new SidebarMenuItem("info", "Offers", OnOffersItemClicked),
            new SidebarMenuItem("profile", "Clients", OnClientsItemClicked)
        });

        SidebarMenu = SidebarMenu with { SidebarMenuItems = items };
    }

    public void OnHomeItemClicked()
    {
        SidebarViewService.NavigateToHome();
    }
    
    public void OnOffersItemClicked()
    {
        SidebarViewService.NavigateToOffers();
    }
    
    public void OnClientsItemClicked()
    {
        SidebarViewService.NavigateToClients();
    }

    public void OnLoginItemPressed()
    {
        SidebarViewService.NavigateToSignIn();
    }
    
    public void OnLogoutButtonPressed()
    {
        SidebarViewService.NavigateToSignOut();
    }
}