namespace Boatman.DataAccess.Identity.Implementations;

public class JwtSettings
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int ExpiresInDays { get; set; }
    public int ExpiresInMinutes { get; set; }
}