using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartments;

public class GetMyApartmentsRequest : IRequest<Response<IEnumerable<Apartment>>>
{
    public GetMyApartmentsRequest(string ownerId)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; private set; }
}