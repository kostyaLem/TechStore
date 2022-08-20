using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechStore.DAL.Context;

internal class TechStoreContextFactory : IDesignTimeDbContextFactory<TechStoreContext>
{
    public TechStoreContext CreateDbContext() => CreateDbContext(Array.Empty<string>());

    public TechStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TechStoreContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechStoreDb;Trusted_Connection=True;");

        return new TechStoreContext(optionsBuilder.Options);
    }
}
