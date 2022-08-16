using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Models;

namespace TechStore.DAL.Configurations;

internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.OrderId, x.ProductId });

        builder.HasOne<Product>(x=>x.Product)
            .WithMany(x=>x.OrderProducts)
            .HasForeignKey(x => x.ProductId);
    }
}
