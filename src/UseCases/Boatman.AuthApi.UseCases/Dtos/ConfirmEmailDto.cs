using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthApi.UseCases.Dtos;

public class ConfirmEmailDto
{
    [Required]
    public string UserId { get; set; } = default!;
    [Required]
    public string Token { get; set; } = default!;
}