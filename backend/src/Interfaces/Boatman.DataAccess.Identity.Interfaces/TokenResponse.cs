namespace Boatman.DataAccess.Identity.Interfaces;

public class TokenResponse
{
    public string? Token { get; set; }
    public DateTime? Expires { get; set; }
}