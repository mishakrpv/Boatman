using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ConfirmEmail;

public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest, Response>
{
    private readonly IAuthService _authService;

    public ConfirmEmailRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.ConfirmEmailAsync(request.Dto);

        return response;
    }
}