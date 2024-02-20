using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Entities.Models.CustomerAggregate;
using Boatman.Entities.Models.OwnerAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Boatman.DataAccess.Identity.Implementations;

public class IdentityContextSeed
{
    public static async Task SeedAsync(IdentityContext identityContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        if (identityContext.Database.IsNpgsql())
        {
            await identityContext.Database.MigrateAsync();
        }

        var roles = GetRoles();
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
        
        var adminEmail = config["AdminEmail"] ?? "dumbGuy@microsoft.com";
        var adminPwd = config["AdminPwd"] ?? "someKindOfPassword123";

        var adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, adminPwd);

        adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser != null)
        {
            foreach (var role in roles)
            {
                await userManager.AddToRoleAsync(adminUser, role);
            }
        }
    }

    private static List<string> GetRoles()
    {
        return
        [
            nameof(Owner),
            nameof(Customer),
            "Admin"
        ];
    }
}