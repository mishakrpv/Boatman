using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.RemoveFromWishlist;

public class RemoveFromWishlistRequest : IRequest<Response>
{
    public RemoveFromWishlistRequest(int apartmentId, string customerId)
    {
        ApartmentId = apartmentId;
        CustomerId = customerId;
    }
    
    public int ApartmentId { get; private set; }
    public string CustomerId { get; private set; }
}