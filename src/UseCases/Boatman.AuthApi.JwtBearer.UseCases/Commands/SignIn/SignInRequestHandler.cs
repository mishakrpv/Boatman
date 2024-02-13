using Boatman.DataAccess.Identity.Interfaces;
using Boatman.TokenService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boatman.AuthApi.UseCases.Commands.SignIn;

public class SignInRequestHandler : IRequestHandler<SignInRequest, TokenPair?>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public SignInRequestHandler(UserManager<ApplicationUser> userManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<TokenPair?> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var user = await _userManager.FindByEmailAsync(dto.Email);
        
        if (user == null)
            return null;

        if (user.PasswordHash != dto.PasswordHash)
            return null;

        var tokenPair = await _tokenService.GetTokenPairAsync(dto.Email);
        user.RefreshToken = tokenPair.RefreshToken;

        return tokenPair;
    }
}