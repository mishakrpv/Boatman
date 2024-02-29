using Boatman.AuthService.Interfaces;
using Boatman.BlobStorage.Implementations.AmazonS3;
using Boatman.BlobStorage.Interfaces;
using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.DataAccess.Interfaces;
using Boatman.Emailing.Implementations.SendGrid;
using Boatman.Emailing.Interfaces;
using Boatman.Logging.Implementations;
using Boatman.Logging.Interfaces;
using Boatman.ProfileService.Interfaces;

namespace Boatman.WebHost.Configurations;

public static class ConfigureInterfaceAdapters
{
    public static IServiceCollection AddInterfaceAdapters(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IAuthService, AuthService.Implementations.Jwt.Identity.AuthService>();
        services.AddScoped<IProfileService, ProfileService.Implementations.Identity.ProfileService>();
        
        services.AddScoped<IBlobStorage, AmazonS3BlobStorage>();
        services.AddTransient<IEmailSender, SendGridEmailSender>();
        services.AddSingleton<ICache, Caching.Implementations.Redis.RedisCache>();
        
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}