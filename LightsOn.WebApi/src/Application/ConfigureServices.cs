using System.Reflection;
using LightsOn.Application.CategoryExpense.Commands.CreateCategoryExpense;
using LightsOn.Application.CategoryExpense.Commands.DeleteCategoryExpense;
using LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;
using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;
using LightsOn.Application.CategoryExpense.Queries.GetCategoryExpenses;
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.DeleteClient;
using LightsOn.Application.Client.Commands.UpdateClient;
using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.Client.Queries.GetClients;
using LightsOn.Application.Common.Behaviours;
using LightsOn.Application.CompanyPhoneNumber.Commands.CreateCompanyPhoneNumber;
using LightsOn.Application.CompanyPhoneNumber.Commands.DeleteCompanyPhoneNumber;
using LightsOn.Application.CompanyPhoneNumber.Queries.GetCompanyPhoneNumbers;
using LightsOn.Application.Customer.Commands.CreateCustomer;
using LightsOn.Application.Customer.Commands.DeleteCustomer;
using LightsOn.Application.Customer.Commands.UpdateCustomer;
using LightsOn.Application.Customer.Queries.GetByIdCustomer;
using LightsOn.Application.Customer.Queries.GetCustomers;
using LightsOn.Application.Engine.Commands.CreateEngine;
using LightsOn.Application.Engine.Commands.DeleteEngine;
using LightsOn.Application.Engine.Commands.UpdateEngine;
using LightsOn.Application.Engine.Queries.GetByIdEngine;
using LightsOn.Application.Engine.Queries.GetEngines;
using LightsOn.Application.Estimate.Commands.CreateEstimate;
using LightsOn.Application.Estimate.Commands.DeleteEstimate;
using LightsOn.Application.Estimate.Commands.UpdateEstimate;
using LightsOn.Application.Estimate.Queries.GetByIdEstimate;
using LightsOn.Application.Estimate.Queries.GetEstimates;
using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Commands.DeleteMaterial;
using LightsOn.Application.Material.Commands.UpdateMaterial;
using LightsOn.Application.Material.Queries.GetByIdMaterial;
using LightsOn.Application.Material.Queries.GetMaterials;
using LightsOn.Application.PowerEquipment.Commands.CreatePowerEquipment;
using LightsOn.Application.PowerEquipment.Commands.DeletePowerEquipment;
using LightsOn.Application.PowerEquipment.Commands.UpdatePowerEquipment;
using LightsOn.Application.PowerEquipment.Queries.GetByIdPowerEquipment;
using LightsOn.Application.PowerEquipment.Queries.GetPowerEquipments;
using LightsOn.Application.ServiceDescription.Commands.CreateServiceDescription;
using LightsOn.Application.ServiceDescription.Commands.DeleteServiceDescription;
using LightsOn.Application.ServiceDescription.Queries.GetServiceDescriptions;
using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Commands.DeleteUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Commands.UpdateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetByNameUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetUnitMeasurements;
using LightsOn.Application.WorkPerformanceDescription.Commands.CreateWorkPerformanceDescription;
using LightsOn.Application.WorkPerformanceDescription.Commands.DeleteWorkPerformanceDescription;
using LightsOn.Application.WorkPerformanceDescription.Commands.UpdateWorkPerformanceDescription;
using LightsOn.Application.WorkPerformanceDescription.Queries.GetByIdWorkPerformanceDescription;
using LightsOn.Application.WorkPerformanceDescription.Queries.GetWorkPerformanceDescriptions;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOn.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        services.AddTransient<ICreateCategoryExpenseCommandHandlerStorageBroker, CreateCategoryExpenseCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteCategoryExpenseCommandHandlerStorageBroker, DeleteCategoryExpenseCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateCategoryExpenseCommandHandlerStorageBroker, UpdateCategoryExpenseCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdCategoryExpenseQueryHandlerStorageBroker, GetByIdCategoryExpenseQueryHandlerStorageBroker>();
        services.AddTransient<IGetCategoryExpensesQueryHandlerStorageBroker, GetCategoryExpensesQueryHandlerStorageBroker>();

        services.AddTransient<ICreateClientCommandHandlerStorageBroker, CreateClientCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteClientCommandHandlerStorageBroker, DeleteClientCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateClientCommandHandlerStorageBroker, UpdateClientCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdClientStorageBroker, GetByIdClientStorageBroker>();
        services.AddTransient<IGetClientsQueryStorageBroker, GetClientsQueryStorageBroker>();
        
        services.AddTransient<ICreateCompanyPhoneNumberCommandHandlerStorageBroker, CreateCompanyPhoneNumberCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteCompanyPhoneNumberCommandHandlerStorageBroker, DeleteCompanyPhoneNumberCommandHandlerStorageBroker>();
        services.AddTransient<IGetCompanyPhoneNumbersQueryStorageBroker, GetCompanyPhoneNumbersQueryStorageBroker>();
        
        services.AddTransient<ICreateCustomerCommandHandlerStorageBroker, CreateCustomerCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteCustomerCommandHandlerStorageBroker, DeleteCustomerCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateCustomerCommandHandlerStorageBroker, UpdateCustomerCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdCustomerStorageBroker, GetByIdCustomerStorageBroker>();
        services.AddTransient<IGetCustomersQueryStorageBroker, GetCustomersQueryStorageBroker>();
        
        services.AddTransient<ICreateEngineCommandHandlerStorageBroker, CreateEngineCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteEngineCommandHandleStorageBroker, DeleteEngineCommandHandleStorageBroker>();
        services.AddTransient<IUpdateEngineCommandHandlerStorageBroker, UpdateEngineCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdEngineHandlerStorageBroker, GetByIdEngineHandlerStorageBroker>();
        services.AddTransient<IGetEnginesHandlerQueryStorageBroker, GetEnginesHandlerQueryStorageBroker>();
        
        services.AddTransient<ICreateEstimateCommandHandlerStorageBroker, CreateEstimateCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteEstimateCommandHandlerStorageBroker, DeleteEstimateCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateEstimateHandlerCommandStorageBroker, UpdateEstimateHandlerCommandStorageBroker>();
        services.AddTransient<IGetByIdEstimateQueryHandlerStorageBroker, GetByIdEstimateQueryHandlerStorageBroker>();
        services.AddTransient<IGetEstimatesQueryHandlerStorageBroker, GetEstimatesQueryHandlerStorageBroker>();
        
        services.AddTransient<ICreateMaterialCommandHandlerStorageBroker, CreateMaterialCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteMaterialCommandHandlerStorageBroker, DeleteMaterialCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateMaterialCommandHandlerStorageBroker, UpdateMaterialCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdMaterialQueryHandlerStorageBroker, GetByIdMaterialQueryHandlerStorageBroker>();
        services.AddTransient<IGetMaterialQueryHandlerStorageBroker, GetMaterialQueryHandlerStorageBroker>();
        
        services.AddTransient<ICreatePowerEquipmentQueryHandlerStorageBroker, CreatePowerEquipmentQueryHandlerStorageBroker>();
        services.AddTransient<IDeletePowerEquipmentCommandHandlerStorageBroker, DeletePowerEquipmentCommandHandlerStorageBroker>();
        services.AddTransient<IUpdatePowerEquipmentCommandHandlerStorageBroker, UpdatePowerEquipmentCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdPowerEquipmentQueryHandlerStorageBroker, GetByIdPowerEquipmentQueryHandlerStorageBroker>();
        services.AddTransient<IGetPowerEquipmentsQueryHandlerStorageBroker, GetPowerEquipmentsQueryHandlerStorageBroker>();
        
        services.AddTransient<ICreateServiceDescriptionCommandHandlerStorageBroker, CreateServiceDescriptionCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteServiceDescriptionCommandHandlerStorageBroker, DeleteServiceDescriptionCommandHandlerStorageBroker>();
        services.AddTransient<IGetServiceDescriptionsQueryStorageBroker, GetServiceDescriptionsQueryStorageBroker>();
        
        services.AddTransient<ICreateUnitMeasurementCommandHandlerStorageBroker, CreateUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteUnitMeasurementCommandHandlerStorageBroker, DeleteUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateUnitMeasurementCommandHandlerStorageBroker, UpdateUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdUnitMeasurementQueryHandlerStorageBroker, GetByIdUnitMeasurementQueryHandlerStorageBroker>();
        services.AddTransient<IGetUnitMeasurementsQueryHandlerStorageBroker, GetUnitMeasurementsQueryHandlerStorageBroker>();
        
        services.AddTransient<ICreateUnitMeasurementCommandHandlerStorageBroker, CreateUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteUnitMeasurementCommandHandlerStorageBroker, DeleteUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateUnitMeasurementCommandHandlerStorageBroker, UpdateUnitMeasurementCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdUnitMeasurementQueryHandlerStorageBroker, GetByIdUnitMeasurementQueryHandlerStorageBroker>();
        services.AddTransient<IGetByNameUnitMeasurementQueryHandlerStorageBroker, GetByNameUnitMeasurementQueryHandlerStorageBroker>();
        services.AddTransient<IGetUnitMeasurementsQueryHandlerStorageBroker, GetUnitMeasurementsQueryHandlerStorageBroker>();
        
        services.AddTransient<ICreateWorkPerformanceDescriptionCommandHandlerStorageBroker, CreateWorkPerformanceDescriptionCommandHandlerStorageBroker>();
        services.AddTransient<IDeleteWorkPerformanceDescriptionCommandHandlerStorageBroker, DeleteWorkPerformanceDescriptionCommandHandlerStorageBroker>();
        services.AddTransient<IUpdateWorkPerformanceDescriptionCommandHandlerStorageBroker, UpdateWorkPerformanceDescriptionCommandHandlerStorageBroker>();
        services.AddTransient<IGetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker, GetByIdWorkPerformanceDescriptionQueryHandlerStorageBroker>();
        services.AddTransient<IGetWorkPerformanceDescriptionsQueryHandlerStorageBroker, GetWorkPerformanceDescriptionsQueryHandlerStorageBroker>();

        return services;
    }
}
