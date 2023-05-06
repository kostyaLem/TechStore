using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechStore.DAL.Context;

internal class TechStoreContextFactory : IDesignTimeDbContextFactory<TechStoreContext>
{
    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public TechStoreContext CreateDbContext() => CreateDbContext(Array.Empty<string>());

    public TechStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TechStoreContext>();
        optionsBuilder.UseSqlServer(ConnectionString);

        return new TechStoreContext(optionsBuilder.Options);
    }
}
