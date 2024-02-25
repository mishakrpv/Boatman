using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity;

public class ApplicationUser : IdentityUser
{
    [ProtectedPersonalData]
    [StringLength(50, MinimumLength = 2)]
    public string? FirstName { get; set; }
    [ProtectedPersonalData]
    [StringLength(50, MinimumLength = 2)]
    public string? MiddleName { get; set; }
    [ProtectedPersonalData]
    [StringLength(50, MinimumLength = 2)]
    public string? LastName { get; set; }
    [ProtectedPersonalData]
    [StringLength(250)]
    public string? Bio { get; set; }
}