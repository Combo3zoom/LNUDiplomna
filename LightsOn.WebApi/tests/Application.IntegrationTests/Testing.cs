using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Constants;
using LightsOn.Infrastructure.Data;
using LightsOn.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOn.Application.IntegrationTests;


public partial class Testing : IAsyncLifetime
{
    private static ITestDatabase _database = null!;
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;
    public Mock<IDateTimeOffSet> _mockDataTimeOffset = null!;
    public Mock<ITelegramBot> _mockTelegramBot = null!;
    
    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _mockDataTimeOffset = new Mock<IDateTimeOffSet>();
        _mockTelegramBot = new Mock<ITelegramBot>();
        _factory = new CustomWebApplicationFactory(_database.GetConnection(), _mockDataTimeOffset, _mockTelegramBot);
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetUserId()
    {
        return _userId;
    }

    public async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { Roles.Administrator });
    }

    public async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _userId = user.Id;

            return _userId;
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception) 
        {
        }

        _userId = null;
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }
    
    public async Task<TEntity?> GetMaterialWithIncludesAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var key = keyValues.First();

        return await context.Set<TEntity>()
            .Include(e => (e as Domain.Entities.Material)!.UnitMeasurement)
            .Include(e => (e as Domain.Entities.Material)!.Estimates)
            .FirstOrDefaultAsync(entity => EF.Property<object>(entity, "Id") == key);
    }
    
    public async Task<TEntity?> GetEstimateWithIncludesAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var key = keyValues.First();

        return await context.Set<TEntity>()
            .Include(e => (e as Domain.Entities.Estimate)!.CategoryExpense)
            .Include(e => (e as Domain.Entities.Estimate)!.Material)
            .FirstOrDefaultAsync(entity => EF.Property<object>(entity, "Id") == key);
    }


    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}



