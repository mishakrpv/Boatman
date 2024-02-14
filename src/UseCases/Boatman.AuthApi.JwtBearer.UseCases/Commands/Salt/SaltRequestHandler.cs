using Boatman.DataAccess.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boatman.AuthApi.UseCases.Commands.Salt;

public class SaltRequestHandler : IRequestHandler<SaltRequest, string?>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SaltRequestHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> Handle(SaltRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null) return null;

        return user.Salt;
    }
}