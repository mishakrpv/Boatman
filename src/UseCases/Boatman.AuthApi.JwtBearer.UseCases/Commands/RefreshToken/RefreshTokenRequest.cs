using Boatman.TokenService.Interfaces;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.RefreshToken;

public class RefreshTokenRequest : IRequest<TokenPair?>
{
    public TokenPair TokenPair { get; private set; }

    public RefreshTokenRequest(TokenPair tokenPair)
    {
        TokenPair = tokenPair;
    }
}