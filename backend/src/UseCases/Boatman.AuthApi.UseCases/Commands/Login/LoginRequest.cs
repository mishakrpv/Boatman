using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Login;

public class LoginRequest : IRequest<Response<TokenDto>>
{
    public LoginRequest(LoginDto dto)
    {
        Dto = dto;
    }

    public LoginDto Dto { get; private set; }
}