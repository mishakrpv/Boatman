using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity;

public class ApplicationContextSeed
{
    private const string AdminRoleName = "Admin";    

    public static async Task SeedAsync(ApplicationContext identityContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        if (identityContext.Database.IsNpgsql())
        {
            await identityContext.Database.MigrateAsync();
        }
        
        var adminEmail = config["AdminEmail"] ?? "admin@example.com";
        var adminPwd = config["AdminPwd"] ?? "123456";

        var adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, adminPwd);

        adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (!await roleManager.RoleExistsAsync(AdminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
        }

        if (adminUser != null)
        {
            await userManager.AddToRoleAsync(adminUser, AdminRoleName);
        }
    }
}