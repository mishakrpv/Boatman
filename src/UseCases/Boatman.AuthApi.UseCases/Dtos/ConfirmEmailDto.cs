using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Boatman.AuthApi.UseCases.Dtos;

public class ConfirmEmailDto
{
    [Required]
    public string Id { get; set; } = default!;
    [Required]
    public string Token { get; set; } = default!;
}