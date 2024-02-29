using Boatman.AuthService.Interfaces;
using Boatman.BlobStorage.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ProfilePhotoAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Common.UseCases.Commands.EditProfilePhoto;

public class EditProfilePhotoHandler : IRequestHandler<EditProfilePhoto, Response>
{
    private readonly IBlobStorage _blob;
    private readonly IAuthService _authService;
    private readonly IRepository<ProfilePhoto> _profilePhotoRepo;

    public EditProfilePhotoHandler(IBlobStorage blob,
        IAuthService authService,
        IRepository<ProfilePhoto> profilePhotoRepo)
    {
        _blob = blob;
        _authService = authService;
        _profilePhotoRepo = profilePhotoRepo;
    }

    public async Task<Response> Handle(EditProfilePhoto request, CancellationToken cancellationToken)
    {
        var response = _authService.GetUserIdByPrincipal(request.Principal);

        if (response.StatusCode != 200)
            return response;

        var profilePhoto = await _profilePhotoRepo.GetByIdAsync(response.Value!);
        
        var blobResponse = await _blob.UploadAsync(request.Photo);

        if (blobResponse.StatusCode != 200)
            return blobResponse;

        if (profilePhoto == null)
        {
            await _profilePhotoRepo.AddAsync(new ProfilePhoto(response.Value!, blobResponse.Value!));
        }
        else
        {
            profilePhoto.UpdateUri(blobResponse.Value!);
        }

        return new Response
        {
            Message = "Profile photo has been edited."
        };
    }
}