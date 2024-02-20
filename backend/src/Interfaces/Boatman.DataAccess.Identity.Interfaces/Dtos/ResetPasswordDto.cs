using System.ComponentModel.DataAnnotations;

namespace Boatman.DataAccess.Identity.Interfaces.Dtos;

public class ResetPasswordDto
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Token { get; set; } = default!;
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; } = default!;
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; } = default!;
}