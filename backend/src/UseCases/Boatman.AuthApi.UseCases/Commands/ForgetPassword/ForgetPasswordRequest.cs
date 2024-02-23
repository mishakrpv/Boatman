using Boatman.Utils;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ForgetPassword;

public class ForgetPasswordRequest : IRequest<Response>
{
    public ForgetPasswordRequest(string email)
    {
        Email = email;
    }
    
    public string Email { get; private set; }
}