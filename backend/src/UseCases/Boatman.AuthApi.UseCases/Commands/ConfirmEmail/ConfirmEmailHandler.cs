using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ConfirmEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmail, Response>
{
    private readonly IAuthService _authService;

    public ConfirmEmailHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(ConfirmEmail request, CancellationToken cancellationToken)
    {
        var response = await _authService.ConfirmEmailAsync(request.Dto);

        return response;
    }
}