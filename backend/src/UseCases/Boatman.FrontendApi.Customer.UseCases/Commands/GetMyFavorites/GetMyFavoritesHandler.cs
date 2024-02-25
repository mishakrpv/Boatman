using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.GetMyFavorites;

public class GetMyFavoritesHandler : IRequestHandler<GetMyFavorites, Response<Favorites>>
{
    private readonly IRepository<Favorites> _favoritesRepo;

    public GetMyFavoritesHandler(IRepository<Favorites> favoritesRepo)
    {
        _favoritesRepo = favoritesRepo;
    }

    public async Task<Response<Favorites>> Handle(Commands.GetMyFavorites.GetMyFavorites request, CancellationToken cancellationToken)
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