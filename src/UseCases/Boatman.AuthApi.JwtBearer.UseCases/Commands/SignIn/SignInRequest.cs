using Boatman.AuthApi.UseCases.Dtos;
using Boatman.TokenService.Interfaces;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.SignIn;

public class SignInRequest : IRequest<TokenPair?>
{
    public SignInRequest(SignInDto dto)
    {
        Dto = dto;
    }

    public SignInDto Dto { get; private set; }
}