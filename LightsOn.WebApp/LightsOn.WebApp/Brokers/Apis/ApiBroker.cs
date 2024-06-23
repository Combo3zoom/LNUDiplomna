using System.Net.Http.Json;
using System.Text.Json;
using LanguageExt;
using LightsOn.WebApp.HttpClients;
using LightsOn.WebApp.Models.Customer;
using LightsOn.WebApp.Models.PhoneNumber;
using LightsOn.WebApp.Models.ServiceDescription;
using static LanguageExt.Prelude;

namespace LightsOn.WebApp.Brokers.Apis;

public class ApiBroker : IApiBroker
{
    private readonly ApiHttpClient _apiHttpClient;

    public ApiBroker(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public TryAsync<int> CreateCustomer(CreateCustomerCommand command)
    {
        return TryAsync(_apiHttpClient.PostAsJsonAsync("customers", command))
            .MapAsync(message => message.Content.ReadFromJsonAsync<int>());
    }

    public TryAsync<CompanyPhoneNumber[]> GetCompanyPhoneNumbers()
    {
        return TryAsync(_apiHttpClient.GetFromJsonAsync<CompanyPhoneNumber[]>("company-phone-numbers"));
    }

    public TryAsync<ServiceDescription[]> GetServiceDescriptions()
    {
        return TryAsync(_apiHttpClient.GetFromJsonAsync<ServiceDescription[]>("service-descriptions"));
    }
}