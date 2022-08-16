using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Models;

namespace TechStore.DAL.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedOn)
            .IsRequired();

        builder.HasOne<Customer>(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);
        builder.HasOne<Employee>(x => x.Employee)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.EmployeeId);
        builder.HasMany<OrderProduct>(x => x.OrderProducts)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);
    }
}
