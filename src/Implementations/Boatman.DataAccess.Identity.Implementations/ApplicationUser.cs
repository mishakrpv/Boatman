using Microsoft.AspNetCore.Identity;

namespace Boatman.DataAccess.Identity.Implementations;

public class ApplicationUser : IdentityUser
{
    public string Salt { get; set; } = default!;
}