using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Models;

namespace TechStore.DAL.Configurations;

internal class ProductStoredImageConfiguration : IEntityTypeConfiguration<ProductStoredImage>
{
    public void Configure(EntityTypeBuilder<ProductStoredImage> builder)
    {
        builder.HasKey(x => new { x.ProductId, x.StoredImageId });
    }
}