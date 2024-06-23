using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LightsOn.WebApp;
using LightsOn.WebApp.Brokers.Apis;
using LightsOn.WebApp.Brokers.Navigations;
using LightsOn.WebApp.HttpClients;
using LightsOn.WebApp.Models.Configurations;
using LightsOn.WebApp.Services.Views.SidebarView;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<ApiConfigurations>(builder.Configuration.GetSection("ApiConfigurations"));

builder.Services.AddScoped(sp 
    => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    var azureAdB2C = builder.Configuration.GetSection("AzureAdB2C");
    options.ProviderOptions.Authentication.Authority = $"{azureAdB2C["Instance"]}/{azureAdB2C["Domain"]}/{azureAdB2C["SignUpSignInPolicyId"]}";
    options.ProviderOptions.Authentication.ClientId = azureAdB2C["ClientId"];
    options.ProviderOptions.Authentication.PostLogoutRedirectUri = azureAdB2C["SignedOutCallbackPath"];
    options.ProviderOptions.DefaultAccessTokenScopes.Add($"{azureAdB2C["Instance"]}/.default");
    options.ProviderOptions.LoginMode = "redirect";
});


builder.Services.AddHttpClient<ApiHttpClient>(
    (provider, client) =>
    {
        var optionsApiConfigurations = provider.GetRequiredService<IOptions<ApiConfigurations>>();
        var baseAddress = optionsApiConfigurations.Value.Url;
        client.BaseAddress = new Uri(baseAddress);  
    });
builder.Services.AddScoped(sp =>
{
    var url = builder.Configuration["ApiConfigurations:Url"];
    
    return new ApiHttpClient
    {
        BaseAddress = new Uri(url)
    };
});

builder.Services.AddTransient<IApiBroker, ApiBroker>();
builder.Services.AddTransient<INavigationBroker, NavigationBroker>();
builder.Services.AddTransient<ISidebarViewService, SidebarViewService>();

builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration.GetValue<string>("SyncfusionLicenseKey"));

builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.Latin1Supplement);
});

var host = builder.Build();

await host.RunAsync();