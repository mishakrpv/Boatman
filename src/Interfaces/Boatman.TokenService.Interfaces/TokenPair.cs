namespace Boatman.TokenService.Interfaces;

public class TokenPair
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}