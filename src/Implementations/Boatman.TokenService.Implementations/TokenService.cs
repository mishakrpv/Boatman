using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Boatman.DataAccess.Identity.Implementations;
using Boatman.TokenService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Boatman.TokenService.Implementations;

public class TokenService : ITokenService
{
    private readonly JwtSettings _settings;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(IOptions<JwtSettings> options,
        UserManager<ApplicationUser> userManager)
    {
        _settings = options.Value;
        _userManager = userManager;
    }
    
    public async Task<string> GetAccessTokenAsync(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyString = _settings.Key;
        var keyBytes = Encoding.UTF8.GetBytes(Guard.Against.NullOrEmpty(keyString));
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(Guard.Against.Null(user));
        
        var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public string GetRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        
        return Convert.ToBase64String(bytes);
    }

    public async Task<TokenPair> GetTokenPairAsync(string email)
    {
        return new TokenPair()
        {
            AccessToken = await GetAccessTokenAsync(email),
            RefreshToken = GetRefreshToken()
        };
    }
}