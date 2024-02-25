using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartments;

public class GetMyApartments : IRequest<Response<IEnumerable<Apartment>>>
{
    public GetMyApartments(string ownerId)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; private set; }
}