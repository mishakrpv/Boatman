using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthApi.UseCases.Dtos;

public class SignUpAsOwnerDto
{
    [Required] public string Email { get; set; } = default!;

    [Required] public string Password { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Bio { get; set; } = default!;
}