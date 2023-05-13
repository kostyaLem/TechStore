using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Models;

namespace TechStore.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Login).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();

        builder
            .HasOne<Employee>()
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.Id);

        builder
            .HasOne<Customer>()
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.Id);

        builder.HasIndex(x => x.Login);
    }
}
