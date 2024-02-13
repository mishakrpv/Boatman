using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Salt;

public class SaltRequest : IRequest<string?>
{
    public string Email { get; private set; }

    public SaltRequest(string email)
    {
        Email = email;
    }
}