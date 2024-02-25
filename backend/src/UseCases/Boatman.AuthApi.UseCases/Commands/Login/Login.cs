using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Login;

public class Login : IRequest<Response<TokenDto>>
{
    public Login(LoginDto dto)
    {
        Dto = dto;
    }

    public LoginDto Dto { get; private set; }
}