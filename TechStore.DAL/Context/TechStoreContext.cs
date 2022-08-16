using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechStore.Domain.Models;

namespace TechStore.DAL.Context;
public class TechStoreContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<StoredImage> StoredImages { get; set; }

    public TechStoreContext()
    {

    }

    public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
