using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthService.Interfaces.Dtos;

public class ConfirmEmailDto
{
    [Required]
    public string UserId { get; set; } = default!;
    [Required]
    public string Token { get; set; } = default!;
}