using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Deputies.Adapter.Out.EFCoreSqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeputiesDbContext>
{
    public DeputiesDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DeputiesDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeputiesSqlServerConnection"));

        return new DeputiesDbContext(optionsBuilder.Options);
    }
}