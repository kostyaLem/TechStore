using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Models;

namespace TechStore.DAL.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.Birthday)
            .HasColumnType("date");

        builder.HasIndex(x => new { x.FirstName, x.LastName, x.Phone });

        builder.HasOne(x => x.User)
            .WithOne();

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
