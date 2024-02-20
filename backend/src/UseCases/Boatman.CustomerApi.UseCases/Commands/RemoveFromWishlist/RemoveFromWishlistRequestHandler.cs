using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.WishlistAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.RemoveFromWishlist;

public class RemoveFromWishlistRequestHandler : IRequestHandler<RemoveFromWishlistRequest, Response>
{
    private readonly IRepository<Wishlist> _wishlistRepo;

    public RemoveFromWishlistRequestHandler(IRepository<Wishlist> wishlistRepo)
    {
        _wishlistRepo = wishlistRepo;
    }

    public async Task<Response> Handle(RemoveFromWishlistRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersWishlistSpecification(request.CustomerId);
        var wishlist = await _wishlistRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (wishlist == null)
            return new Response
            {
                StatusCode = 404,
                Message = "User has no wishlist."
            };
        
        wishlist.RemoveItem(request.ApartmentId);

        await _wishlistRepo.UpdateAsync(wishlist, cancellationToken);
        
        return new Response
        {
            Message = "Apartment has been successfully removed from the wishlist."
        };
    }
}