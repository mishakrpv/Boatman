using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.AddToFavorites;

public class AddToFavoritesHandler : IRequestHandler<AddToFavorites, Response>
{
    private readonly IRepository<Favorites> _favoritesRepo;
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public AddToFavoritesHandler(IRepository<Favorites> favoritesRepo,
        IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _favoritesRepo = favoritesRepo;
        _apartmentRepo = apartmentRepo;
        _cache = cache;
    }

    public async Task<Response> Handle(AddToFavorites request, CancellationToken cancellationToken)
    {
        var spec = new CustomersFavoritesSpecification(request.CustomerId);
        var favorites = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken)
                       ?? await _favoritesRepo.AddAsync(new Favorites(request.CustomerId), cancellationToken);

        var apartment = await _cache.GetAsync<Apartment>(
            CacheHelpers.GenerateApartmentCacheKey(request.ApartmentId));

        if (apartment == null)
        {
            apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

            if (apartment == null)
                return new Response
                {
                    StatusCode = 404,
                    Message = "Apartment not found."
                };
        }
        
        favorites.AddItem(request.ApartmentId);

        await _favoritesRepo.UpdateAsync(favorites, cancellationToken);

        await _cache.SetAsync<Favorites>(CacheHelpers.GenerateFavoritesCacheKey(request.CustomerId), favorites);

        return new Response
        {
            Message = "Apartment has been successfully added to the favorites."
        };
    }
}