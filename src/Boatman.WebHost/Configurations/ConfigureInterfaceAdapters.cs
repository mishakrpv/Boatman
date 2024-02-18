using Boatman.DataAccess.Domain.Implementations;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Identity.Implementations;
using Boatman.DataAccess.Identity.Interfaces;
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

        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IEmailSender, SendGridEmailSender>();

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}