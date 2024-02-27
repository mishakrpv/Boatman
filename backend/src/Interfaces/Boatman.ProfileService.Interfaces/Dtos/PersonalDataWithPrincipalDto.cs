using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Boatman.ProfileService.Interfaces.Dtos;

public class PersonalDataWithPrincipalDto : PersonalDataDto
{
    [Required]
    public ClaimsPrincipal Principal { get; set; } = default!;
}