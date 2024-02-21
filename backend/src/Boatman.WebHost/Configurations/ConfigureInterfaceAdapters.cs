using Boatman.AuthService.Implementations;
using Boatman.AuthService.Interfaces;
using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.DataAccess.Interfaces;
using Boatman.Emailing.Implementations;
using Boatman.Emailing.Interfaces;
using Boatman.Logging.Implementations;
using Boatman.Logging.Interfaces;

namespace Boatman.WebHost.Configurations;

public static class ConfigureInterfaceAdapters
{
    public static IServiceCollection AddInterfaceAdapters(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IAuthService, AuthService.Implementations.AuthService>();
        services.AddTransient<IEmailSender, SendGridEmailSender>();

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}