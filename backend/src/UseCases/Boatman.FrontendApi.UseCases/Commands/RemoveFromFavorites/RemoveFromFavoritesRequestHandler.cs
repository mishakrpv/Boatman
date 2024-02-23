using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.RemoveFromFavorites;

public class RemoveFromFavoritesRequestHandler : IRequestHandler<RemoveFromFavoritesRequest, Response>
{
    private readonly IRepository<Favorites> _favoritesRepo;

    public RemoveFromFavoritesRequestHandler(IRepository<Favorites> favoritesRepo)
    {
        _favoritesRepo = favoritesRepo;
    }

    public async Task<Response> Handle(RemoveFromFavoritesRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersFavoritesSpecification(request.CustomerId);
        var wishlist = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (wishlist == null)
            return new Response
            {
                StatusCode = 404,
                Message = "User has no favorites."
            };
        
        wishlist.RemoveItem(request.ApartmentId);

        await _favoritesRepo.UpdateAsync(wishlist, cancellationToken);
        
        return new Response
        {
            Message = "Apartment has been successfully removed from the favorites."
        };
    }
}