using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.AddToFavorites;

public class AddToFavoritesRequest : IRequest<Response>
{
    public AddToFavoritesRequest(int apartmentId, string customerId)
    {
        ApartmentId = apartmentId;
        CustomerId = customerId;
    }

    public int ApartmentId { get; private set; }
    public string CustomerId { get; private set; }
}