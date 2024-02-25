using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ForgetPassword;

public class ForgetPasswordHandler : IRequestHandler<ForgetPassword, Response>
{
    private readonly IAuthService _authService;

    public ForgetPasswordHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(ForgetPassword request, CancellationToken cancellationToken)
    {
        var response = await _authService.ForgetPasswordAsync(request.Email);

        return response;
    }
}