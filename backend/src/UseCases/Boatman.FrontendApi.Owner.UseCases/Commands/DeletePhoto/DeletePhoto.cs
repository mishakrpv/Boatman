using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.DeletePhoto;

public class DeletePhoto : IRequest<Response>
{
    public DeletePhoto(int apartmentId, int photoId)
    {
        ApartmentId = apartmentId;
        PhotoId = photoId;
    }

    public int ApartmentId { get; private set; }
    public int PhotoId { get; private set; }
}