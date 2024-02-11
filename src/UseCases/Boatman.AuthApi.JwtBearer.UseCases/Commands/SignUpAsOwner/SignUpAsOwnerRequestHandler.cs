using Boatman.TokenService.Interfaces;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;

public class SignUpAsOwnerRequestHandler : IRequestHandler<SignUpAsOwnerRequest, TokenPair>
{
    public async Task<TokenPair> Handle(SignUpAsOwnerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}