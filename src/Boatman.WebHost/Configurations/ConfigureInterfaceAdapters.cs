using Boatman.DataAccess.Domain.Implementations;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Identity.Implementations;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.TokenService.Interfaces;
using Boatman.TokenService.Implementations;

namespace Boatman.WebHost.Configurations;

public static class ConfigureInterfaceAdapters
{
    public static IServiceCollection AddInterfaceAdapters(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddScoped<ITokenService, TokenService.Implementations.TokenService>();

        return services;
    }
}