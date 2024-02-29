using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.GetMyFavorites;

public class GetMyFavoritesHandler : IRequestHandler<GetMyFavorites, Response<Favorites>>
{
    private readonly IRepository<Favorites> _favoritesRepo;
    private readonly ICache _cache;

    public GetMyFavoritesHandler(IRepository<Favorites> favoritesRepo,
        ICache cache)
    {
        _favoritesRepo = favoritesRepo;
        _cache = cache;
    }

    public async Task<Response<Favorites>> Handle(GetMyFavorites request, CancellationToken cancellationToken)
    {
        var favorites = await _cache.GetAsync<Favorites>(CacheHelpers.GenerateFavoritesCacheKey(request.CustomerId));

        if (favorites == null)
        {
            var spec = new CustomersFavoritesSpecification(request.CustomerId);
            favorites = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken);

            if (favorites == null)
                return new Response<Favorites>
                {
                    StatusCode = 404,
                    Message = "Customer has no favorites."
                };
        }

        return new Response<Favorites>(favorites);
    }
}