using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.MyApartments;

public class MyApartmentsRequest : IRequest<IEnumerable<Apartment>>
{
    public string OwnerId { get; private set; }

    public MyApartmentsRequest(string ownerId)
    {
        OwnerId = ownerId;
    }
}