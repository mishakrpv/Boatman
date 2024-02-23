using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ForgetPassword;

public class ForgetPasswordRequestHandler : IRequestHandler<ForgetPasswordRequest, Response>
{
    private readonly IAuthService _authService;

    public ForgetPasswordRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(ForgetPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.ForgetPasswordAsync(request.Email);

        return response;
    }
}