namespace Boatman.AuthService.Interfaces.Dtos;

public class TokenDto
{
    public string? AccessToken { get; set; }
    public DateTime? Expires { get; set; }
}