using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.WishlistAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.AddToWishlist;

public class AddToWishlistRequestHandler : IRequestHandler<AddToWishlistRequest, Response>
{
    private readonly IRepository<Wishlist> _wishlistRepo;

    public AddToWishlistRequestHandler(IRepository<Wishlist> wishlistRepo)
    {
        _wishlistRepo = wishlistRepo;
    }
    
    public async Task<Response> Handle(AddToWishlistRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var spec = new CustomersWishlistSpecification(dto.CustomerId);
        var wishlist = await _wishlistRepo.FirstOrDefaultAsync(spec, cancellationToken)
                       ?? await _wishlistRepo.AddAsync(new Wishlist(dto.CustomerId), cancellationToken);
        
        wishlist.AddItem(dto.ApartmentId);

        await _wishlistRepo.UpdateAsync(wishlist, cancellationToken);

        return new Response
        {
            Message = "Apartment has been successfully added to the wishlist."
        };
    }
}