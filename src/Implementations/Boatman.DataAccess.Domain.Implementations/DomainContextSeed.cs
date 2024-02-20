using Microsoft.EntityFrameworkCore;

namespace Boatman.DataAccess.Domain.Implementations;

public class DomainContextSeed
{
    public static async Task SeedAsync(DomainContext domainContext)
    {
        if (domainContext.Database.IsNpgsql())
        {
            await domainContext.Database.MigrateAsync();
        }
    }
}