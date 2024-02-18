using System.ComponentModel.DataAnnotations;

namespace Boatman.CommonApi.Hubs.Schemes.SendSchemes;

public class SendMessageScheme
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string ToEmail { get; set; } = default!;
    [Required]
    [StringLength(250)]
    public string Message { get; set; } = default!;
}