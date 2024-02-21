using Boatman.AuthService.Interfaces;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ResetPassword;

public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, Response>
{
    private readonly IAuthService _authService;

    public ResetPasswordRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<Response> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.ResetPasswordAsync(request.Dto);

        return response;
    }
}