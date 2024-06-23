using LightsOn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightsOn.Infrastructure.Data.Configurations;

public class WorkPerformanceDescriptionConfiguration : IEntityTypeConfiguration<WorkPerformanceDescription>
{
    public void Configure(EntityTypeBuilder<WorkPerformanceDescription> builder)
    {
        
    }
}