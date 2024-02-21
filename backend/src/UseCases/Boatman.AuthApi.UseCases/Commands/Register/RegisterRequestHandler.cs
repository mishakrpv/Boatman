using Boatman.AuthService.Interfaces;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Register;

public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Response>
{
    private readonly IAuthService _authService;

    public RegisterRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterUserAsync(request.Dto);
        
        return response;
    }
}