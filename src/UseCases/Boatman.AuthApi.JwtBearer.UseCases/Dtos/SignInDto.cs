using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthApi.UseCases.Dtos;

public class SignInDto
{
    [Required] public string Email { get; set; } = default!;

    [Required] public string PasswordHash { get; set; } = default!;
}