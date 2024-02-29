using System.ComponentModel.DataAnnotations;

namespace Boatman.ProfileService.Interfaces.Dtos;

public class PersonalDataDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string? MiddleName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string? LastName { get; set; }
    [Required]
    [StringLength(250)]
    public string? Bio { get; set; }
}