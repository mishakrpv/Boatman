using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ForgetPassword;

public class ForgetPasswordRequestHandler : IRequestHandler<ForgetPasswordRequest, Response>
{
    private readonly IUserService _userService;

    public ForgetPasswordRequestHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Response> Handle(ForgetPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.ForgetPasswordAsync(request.Email);

        return response;
    }
}