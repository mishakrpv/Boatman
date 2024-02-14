using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.MyApartments;

public class MyApartmentsRequest : IRequest<Response<IEnumerable<Apartment>>>
{
    public MyApartmentsRequest(string ownerId)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; private set; }
}