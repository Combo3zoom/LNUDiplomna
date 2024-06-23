using System.Collections.Immutable;
using LightsOn.WebApp.Brokers.Apis;
using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.Service;

public sealed record ServiceMenu(
    ImmutableList<ServiceItem> ServiceItems);
public sealed record ServiceItem(string HeaderText, string MainText, string LowerPriceLimit);
public partial class Service
{
    protected override async Task OnInitializedAsync()
    {
        await ApiBroker.GetServiceDescriptions()
            .Select(serviceDescriptions => serviceDescriptions
                .Select(pn => new ServiceItem(pn.HeaderText, pn.MainText, pn.LowerPriceLimit))
                .ToImmutableList())
            .Match(serviceItems =>
            {
                ServiceMenu = new ServiceMenu(serviceItems);
            }, exception =>
            {
                ServiceMenu = new ServiceMenu(ImmutableList<ServiceItem>.Empty);
            });
    }
    
    public ServiceMenu ServiceMenu { get; set; }
    
    [Inject]
    private IApiBroker ApiBroker { get; set; }
}