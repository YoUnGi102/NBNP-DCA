using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DmContext>
{
    public DmContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DmContext>();
        optionsBuilder.UseSqlite(@"Data Source = VIADatabaseProduction.db");
        return new DmContext(optionsBuilder.Options);
    }
}