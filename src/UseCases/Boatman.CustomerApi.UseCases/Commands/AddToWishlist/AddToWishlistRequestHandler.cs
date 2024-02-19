using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.WishlistAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.AddToWishlist;

public class AddToWishlistRequestHandler : IRequestHandler<AddToWishlistRequest, Response>
{
    private readonly IRepository<Wishlist> _wishlistRepo;
    private readonly IRepository<Apartment> _apartmentRepo;

    public AddToWishlistRequestHandler(IRepository<Wishlist> wishlistRepo,
        IRepository<Apartment> apartmentRepo)
    {
        _wishlistRepo = wishlistRepo;
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(AddToWishlistRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersWishlistSpecification(request.CustomerId);
        var wishlist = await _wishlistRepo.FirstOrDefaultAsync(spec, cancellationToken)
                       ?? await _wishlistRepo.AddAsync(new Wishlist(request.CustomerId), cancellationToken);

        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        wishlist.AddItem(request.ApartmentId);

        await _wishlistRepo.UpdateAsync(wishlist, cancellationToken);

        return new Response
        {
            Message = "Apartment has been successfully added to the wishlist."
        };
    }
}