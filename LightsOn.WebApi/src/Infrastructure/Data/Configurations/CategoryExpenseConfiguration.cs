using LightsOn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightsOn.Infrastructure.Data.Configurations;

public class CategoryExpenseConfiguration : IEntityTypeConfiguration<CategoryExpense>
{
    public void Configure(EntityTypeBuilder<CategoryExpense> builder)
    {
        
    }
}