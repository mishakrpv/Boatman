using Boatman.ProfileService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Common.UseCases.Commands.ChangeEmail;

public class ChangeEmailHandler : IRequestHandler<ChangeEmail, Response>
{
    private readonly IProfileService _profile;

    public ChangeEmailHandler(IProfileService profile)
    {
        _profile = profile;
    }

    public async Task<Response> Handle(ChangeEmail request, CancellationToken cancellationToken)
    {
        var response = await _profile.ChangeEmail(request.Principal, request.Email);

        return response;
    }
}