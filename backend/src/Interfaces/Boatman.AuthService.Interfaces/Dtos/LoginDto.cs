using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthService.Interfaces.Dtos;

public class LoginDto
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; } = default!;
}