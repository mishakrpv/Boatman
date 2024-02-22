using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.DeleteApartment;

public class DeleteApartmentRequest : IRequest<Response>
{
    public DeleteApartmentRequest(int apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public int ApartmentId { get; private set; }
}