using Boatman.AuthService.Interfaces;
using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, Response<TokenDto>>
{
    private readonly IAuthService _authService;

    public LoginRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response<TokenDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.LoginUserAsync(request.Dto);

        return response;
    }
}