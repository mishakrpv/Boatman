using Boatman.DataAccess.Identity.Interfaces;
using Boatman.TokenService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boatman.AuthApi.UseCases.Commands.RefreshToken;

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, TokenPair?>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenRequestHandler(ITokenService tokenService,
        UserManager<ApplicationUser> userManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<TokenPair?> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        string? accessToken = request.TokenPair.AccessToken;
        string? refreshToken = request.TokenPair.RefreshToken;
        
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        if (principal == null)
        {
            return null;
        }

        string username = principal!.Identity!.Name!;
        
        var user = await _userManager.FindByNameAsync(username);
        
        if (user == null || user.RefreshToken != refreshToken)
        {
            return null;
        }
        
        var newAccessToken = await _tokenService.GetAccessTokenAsync(user.Email!);
        var newRefreshToken = _tokenService.GetRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenPair()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}