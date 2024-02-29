using System.Security.Claims;
using Boatman.Utils.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boatman.FrontendApi.Common.UseCases.Commands.EditProfilePhoto;

public class EditProfilePhoto : IRequest<Response>
{
    public EditProfilePhoto(IFormFile photo, ClaimsPrincipal principal)
    {
        Photo = photo;
        Principal = principal;
    }

    public IFormFile Photo { get; private set; }
    public ClaimsPrincipal Principal { get; private set; }
}