using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ResetPassword;

public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, Response>
{
    private readonly IUserService _userService;

    public ResetPasswordRequestHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Response> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.ResetPasswordAsync(request.Dto);

        return response;
    }
}