using LightsOn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightsOn.Infrastructure.Data.Configurations;

public class UnitMeasurementConfiguration : IEntityTypeConfiguration<UnitMeasurement>
{
    public void Configure(EntityTypeBuilder<UnitMeasurement> builder)
    {
        
    }
}