using Boatman.ProfileService.Interfaces;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Common.UseCases.Commands.EditProfile;

public class EditProfileHandler : IRequestHandler<EditProfile, Response>
{
    private readonly IProfileService _profile;

    public EditProfileHandler(IProfileService profile)
    {
        _profile = profile;
    }

    public async Task<Response> Handle(EditProfile request, CancellationToken cancellationToken)
    {
        var response = await _profile.EditProfile(request.Dto);

        return response;
    }
}