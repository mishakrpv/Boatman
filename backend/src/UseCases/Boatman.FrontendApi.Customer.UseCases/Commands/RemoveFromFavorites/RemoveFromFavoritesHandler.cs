using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.RemoveFromFavorites;

public class RemoveFromFavoritesHandler : IRequestHandler<RemoveFromFavorites, Response>
{
    private readonly IRepository<Favorites> _favoritesRepo;
    private readonly ICache _cache;

    public RemoveFromFavoritesHandler(IRepository<Favorites> favoritesRepo,
        ICache cache)
    {
        _favoritesRepo = favoritesRepo;
        _cache = cache;
    }

    public async Task<Response> Handle(RemoveFromFavorites request, CancellationToken cancellationToken)
    {
        var spec = new CustomersFavoritesSpecification(request.CustomerId);
        var favorites = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (favorites == null)
            return new Response
            {
                StatusCode = 404,
                Message = "User has no favorites."
            };
        
        favorites.RemoveItem(request.ApartmentId);

        await _favoritesRepo.UpdateAsync(favorites, cancellationToken);

        await _cache.SetAsync<Favorites>(CacheHelpers.GenerateFavoritesCacheKey(request.CustomerId), favorites);
        
        return new Response
        {
            Message = "Apartment has been successfully removed from the favorites."
        };
    }
}