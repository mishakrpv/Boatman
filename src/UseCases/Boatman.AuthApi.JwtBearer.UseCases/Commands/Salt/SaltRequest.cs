using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Salt;

public class SaltRequest : IRequest<string?>
{
    public SaltRequest(string email)
    {
        Email = email;
    }

    public string Email { get; private set; }
}