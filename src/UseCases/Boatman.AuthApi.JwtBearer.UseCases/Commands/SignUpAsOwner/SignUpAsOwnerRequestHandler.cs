using Boatman.DataAccess.Identity.Interfaces;
using Boatman.TokenService.Interfaces;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;

public class SignUpAsOwnerRequestHandler : IRequestHandler<SignUpAsOwnerRequest, TokenPair>
{
    private readonly IUserManager _userManager;

    public SignUpAsOwnerRequestHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<TokenPair> Handle(SignUpAsOwnerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}