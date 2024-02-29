using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Boatman.ProfileService.Interfaces.Dtos;

public class PersonalDataWithPrincipalDto : PersonalDataDto
{
    public PersonalDataWithPrincipalDto(PersonalDataDto dto)
    {
        FirstName = dto.FirstName;
        MiddleName = dto.MiddleName;
        LastName = dto.LastName;
        Bio = dto.Bio;
    }
    
    [Required]
    public ClaimsPrincipal Principal { get; set; } = default!;
}