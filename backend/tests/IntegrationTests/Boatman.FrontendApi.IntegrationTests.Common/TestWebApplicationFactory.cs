using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Boatman.FrontendApi.IntegrationTests.Common;

public class TestWebApplicationFactory : WebApplicationFactory<WebHost.EntryPoint>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase("ApplicationTestDb");
            });

            services.AddAuthentication(TestDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                    TestDefaults.AuthenticationScheme, options => { });

            services.AddAuthorizationBuilder()
                .AddDefaultPolicy("Test", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AuthenticationSchemes.Add(TestDefaults.AuthenticationScheme);
                });
        });
    }
}