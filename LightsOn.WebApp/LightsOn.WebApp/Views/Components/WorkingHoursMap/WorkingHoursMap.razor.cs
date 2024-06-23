using System.Collections.Immutable;
using LightsOn.WebApp.Brokers.Apis;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Maps;

namespace LightsOn.WebApp.Views.Components.WorkingHoursMap;

public sealed record PhoneNumberMenu(
    ImmutableList<PhoneNumberMenuItem> PhoneNumberItems);
public sealed record PhoneNumberMenuItem(string PhoneNumber);

public sealed record StreetMenu(
    ImmutableList<StreetMenuItem> StreetMenuItems);
public sealed record StreetMenuItem(string Street);

public class City
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
}
public partial class WorkingHoursMap  : ComponentBase
{
    public List<ToolbarItem> ToolbarItems  { get; set; }

    public WorkingHoursMap()
    {
        ToolbarItems =
        [
            ToolbarItem.Zoom,
            ToolbarItem.ZoomIn, 
            ToolbarItem.ZoomOut, 
            ToolbarItem.Pan
        ];
    }
    
    protected override async Task OnInitializedAsync()
    {
        await ApiBroker.GetCompanyPhoneNumbers()
            .Select(phoneNumbers =>
            {
                var phoneNumberItems = phoneNumbers
                    .Select(pn => new PhoneNumberMenuItem(FormatPhoneNumber(pn.PhoneNumber)))
                    .ToImmutableList();

                return new PhoneNumberMenu(phoneNumberItems);
            }).Match(menu =>
            {
                PhoneNumberMenu = menu;
            }, exception =>
            {
                PhoneNumberMenu = new PhoneNumberMenu(ImmutableList<PhoneNumberMenuItem>.Empty);
            });
        
        
        var streetItems = ImmutableList.Create(
            items: new[]
            {
                new StreetMenuItem("Львівський р-н., с. Лапаївка, вул. Ярослава Мудрого 24"),
            });
        
        StreetMenu = new StreetMenu(streetItems);
    }
    
    private string FormatPhoneNumber(string rawNumber)
    {
        if (rawNumber.StartsWith("+380"))
            return $"{rawNumber.Substring(0, 4)} {rawNumber.Substring(4, 2)}" +
                   $" {rawNumber.Substring(6, 3)} {rawNumber.Substring(9, 2)}" +
                   $" {rawNumber.Substring(11, 2)}";
        return rawNumber;
    }
    
    private List<City> Cities = new List<City> {
        new City { Latitude = 49.8075994453431, Longitude = 23.905400226487536,  Name="LGenera" },
    };
    
    public PhoneNumberMenu PhoneNumberMenu { get; set; }
    public StreetMenu StreetMenu { get; set; }
    
    [Inject]
    private IApiBroker ApiBroker { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; }
}