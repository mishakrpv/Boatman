using Boatman.Utils;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ForgetPassword;

public class ForgetPassword : IRequest<Response>
{
    public ForgetPassword(string email)
    {
        Email = email;
    }
    
    public string Email { get; private set; }
}