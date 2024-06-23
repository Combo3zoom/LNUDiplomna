using System.Reflection;
using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Entities;
using LightsOn.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightsOn.Infrastructure.Data;

public class  ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CategoryExpense> CategoryExpenses => Set<CategoryExpense>();
    public DbSet<CompanyPhoneNumber> CompanyPhoneNumbers => Set<CompanyPhoneNumber>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Engine> Engines => Set<Engine>();
    public DbSet<Estimate> Estimates => Set<Estimate>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<PowerEquipment> PowerEquipments => Set<PowerEquipment>();
    public DbSet<ServiceDescription> ServiceDescriptions => Set<ServiceDescription>();
    public DbSet<UnitMeasurement> UnitMeasurements => Set<UnitMeasurement>();
    public DbSet<WorkPerformanceDescription> WorkPerformanceDescriptions => Set<WorkPerformanceDescription>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    
    public override ValueTask DisposeAsync() {
        base.DisposeAsync();

        return new ValueTask();
     
    }
}
