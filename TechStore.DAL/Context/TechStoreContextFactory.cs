using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechStore.DAL.Context;

internal class TechStoreContextFactory : IDesignTimeDbContextFactory<TechStoreContext>
{
    private const string ConnectionString = "Server=.\\SQLEXPRESS;Database=TechStoreDb;Trusted_Connection=True;";

    public TechStoreContext CreateDbContext() => CreateDbContext(Array.Empty<string>());

    public TechStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TechStoreContext>();
        optionsBuilder.UseSqlServer(ConnectionString);

        return new TechStoreContext(optionsBuilder.Options);
    }
}
