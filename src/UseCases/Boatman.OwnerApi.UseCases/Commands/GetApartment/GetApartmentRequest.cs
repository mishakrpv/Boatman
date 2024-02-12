using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.GetApartment;

public class GetApartmentRequest : IRequest<Apartment>
{
    public int ApartmentId { get; private set; }

    public GetApartmentRequest(int apartmentId)
    {
        ApartmentId = apartmentId;
    }
}