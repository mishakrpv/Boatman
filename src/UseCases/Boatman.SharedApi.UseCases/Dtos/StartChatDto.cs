using System.ComponentModel.DataAnnotations;

namespace Boatman.SharedApi.UseCases.Dtos;

public class StartChatDto
{
    [Required]
    public IEnumerable<string> UserEmails { get; set; } = default!;
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string SenderEmail { get; set; } = default!;
    [Required]
    [StringLength(250)]
    public string Message { get; set; } = default!;
}