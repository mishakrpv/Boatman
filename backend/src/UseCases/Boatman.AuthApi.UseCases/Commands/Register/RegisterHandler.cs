using Boatman.AuthService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Register;

public class RegisterHandler : IRequestHandler<Register, Response>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Response> Handle(Register request, CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterUserAsync(request.Dto);
        
        return response;
    }
}