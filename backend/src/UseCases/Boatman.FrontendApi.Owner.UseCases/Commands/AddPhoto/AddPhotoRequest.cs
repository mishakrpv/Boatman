using Boatman.Utils.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddPhoto;

public class AddPhotoRequest : IRequest<Response<string>>
{
    public AddPhotoRequest(int apartmentId, IFormFile photo)
    {
        ApartmentId = apartmentId;
        Photo = photo;
    }

    public int ApartmentId { get; private set; }
    public IFormFile Photo { get; private set; }
}