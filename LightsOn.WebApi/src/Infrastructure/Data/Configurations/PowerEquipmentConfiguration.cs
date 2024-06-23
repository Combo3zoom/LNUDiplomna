using LightsOn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightsOn.Infrastructure.Data.Configurations;

public class PowerEquipmentConfiguration : IEntityTypeConfiguration<PowerEquipment>
{
    public void Configure(EntityTypeBuilder<PowerEquipment> builder)
    {
        
    }
}