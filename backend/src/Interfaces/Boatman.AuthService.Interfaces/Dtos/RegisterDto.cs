using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthService.Interfaces.Dtos;

public class RegisterDto
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; } = default!;
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; } = default!;
    [StringLength(50, MinimumLength = 2)]
    public string? FirstName { get; set; }
    [StringLength(50, MinimumLength = 2)]
    public string? MiddleName { get; set; }
    [StringLength(50, MinimumLength = 2)]
    public string? LastName { get; set; }
    [StringLength(250)]
    public string? Bio { get; set; }
}