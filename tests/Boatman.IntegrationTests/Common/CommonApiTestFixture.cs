using Boatman.DataAccess.Domain.Implementations;
using Boatman.DataAccess.Identity.Implementations;
using Boatman.WebHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Boatman.IntegrationTests.Common;

public class CommonApiTestFixture : WebApplicationFactory<AssemblyAnchor>
{
    private const string Environment = "Development";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(Environment);

        // Add mock/test services to the builder here
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp => new DbContextOptionsBuilder<DomainContext>()
                .UseInMemoryDatabase("DomainDbForTests")
                .UseApplicationServiceProvider(sp)
                .Options);
            services.AddScoped(sp => new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("IdentityDbForTests")
                .UseApplicationServiceProvider(sp)
                .Options);
        });

        return base.CreateHost(builder);
    }
}