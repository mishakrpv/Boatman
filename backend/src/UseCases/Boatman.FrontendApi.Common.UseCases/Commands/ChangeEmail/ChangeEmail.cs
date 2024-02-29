using System.Security.Claims;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Common.UseCases.Commands.ChangeEmail;

public class ChangeEmail : IRequest<Response>
{
    public ChangeEmail(ClaimsPrincipal principal, string email)
    {
        Principal = principal;
        Email = email;
    }

    public ClaimsPrincipal Principal { get; private set; }
    public string Email { get; private set; }
}