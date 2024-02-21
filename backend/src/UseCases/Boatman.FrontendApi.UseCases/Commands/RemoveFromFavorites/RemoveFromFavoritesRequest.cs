using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.RemoveFromFavorites;

public class RemoveFromFavoritesRequest : IRequest<Response>
{
    public RemoveFromFavoritesRequest(int apartmentId, string customerId)
    {
        ApartmentId = apartmentId;
        CustomerId = customerId;
    }
    
    public int ApartmentId { get; private set; }
    public string CustomerId { get; private set; }
}