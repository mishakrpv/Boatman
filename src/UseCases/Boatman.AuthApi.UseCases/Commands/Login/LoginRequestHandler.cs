using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, Response<TokenResponse>>
{
    private readonly IUserService _userService;

    public LoginRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Response<TokenResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.LoginUserAsync(request.Dto);

        return response;
    }
}