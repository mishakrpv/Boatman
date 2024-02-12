using Microsoft.AspNetCore.Identity;

namespace Boatman.DataAccess.Identity.Interfaces;

public class ApplicationUser : IdentityUser
{
    public string Salt { get; set; } = default!;
}