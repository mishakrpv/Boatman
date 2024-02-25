using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPassword, Response>
{
    private readonly IAuthService _authService;

    public ResetPasswordHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<Response> Handle(ResetPassword request, CancellationToken cancellationToken)
    {
        var response = await _authService.ResetPasswordAsync(request.Dto);

        return response;
    }
}