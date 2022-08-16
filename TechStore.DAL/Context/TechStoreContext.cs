using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TechStore.DAL.Context;
public class TechStoreContext : DbContext
{
    public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
