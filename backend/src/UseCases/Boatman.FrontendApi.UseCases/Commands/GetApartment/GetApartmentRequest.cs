using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.GetApartment;

public class GetApartmentRequest : IRequest<Response<Apartment>>
{
    public GetApartmentRequest(int apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public int ApartmentId { get; private set; }
}