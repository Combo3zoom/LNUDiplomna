using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Models.Clients;
using LightsOn.WebApp.Models.Customer;
using LightsOn.WebApp.Models.PhoneNumber;
using LightsOn.WebApp.Models.ServiceDescription;

namespace LightsOn.WebApp.Brokers.Apis;

public interface IApiBroker
{
    TryAsync<int> CreateCustomer(CreateCustomerCommand command);
    TryAsync<CompanyPhoneNumber[]> GetCompanyPhoneNumbers();
    TryAsync<ServiceDescription[]> GetServiceDescriptions();
}