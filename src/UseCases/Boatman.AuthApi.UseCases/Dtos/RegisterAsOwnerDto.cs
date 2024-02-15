using System.ComponentModel.DataAnnotations;
using Boatman.DataAccess.Identity.Interfaces.Dtos;

namespace Boatman.AuthApi.UseCases.Dtos;

public class RegisterAsOwnerDto : RegisterDto
{
    [StringLength(50, MinimumLength = 2)] public string? FirstName { get; set; }

    [StringLength(50, MinimumLength = 2)] public string? MiddleName { get; set; }

    [StringLength(50, MinimumLength = 2)] public string? LastName { get; set; }

    [StringLength(250)] public string? Bio { get; set; }
}