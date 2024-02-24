using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.GetFavorites;

public class MyFavoritesHandler : IRequestHandler<MyFavorites, Response<Favorites>>
{
    private readonly IRepository<Favorites> _favoritesRepo;

    public MyFavoritesHandler(IRepository<Favorites> favoritesRepo)
    {
        _favoritesRepo = favoritesRepo;
    }

    public async Task<Response<Favorites>> Handle(MyFavorites request, CancellationToken cancellationToken)
    {
        var spec = new CustomersFavoritesSpecification(request.CustomerId);
        var favorites = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (favorites == null)
            return new Response<Favorites>
            {
                StatusCode = 404,
                Message = "Customer has no favorites."
            };

        return new Response<Favorites>(favorites);
    }
}