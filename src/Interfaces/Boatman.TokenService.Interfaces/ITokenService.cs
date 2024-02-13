using System.Security.Claims;

namespace Boatman.TokenService.Interfaces;

public interface ITokenService
{
    Task<string> GetAccessTokenAsync(string email);
    string GetRefreshToken();
    Task<TokenPair> GetTokenPairAsync(string email);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}