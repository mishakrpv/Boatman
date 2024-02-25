using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.RemoveFromFavorites;

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
        var favorites = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (favorites == null)
            return new Response
            {
                StatusCode = 404,
                Message = "User has no favorites."
            };
        
        favorites.RemoveItem(request.ApartmentId);

        await _favoritesRepo.UpdateAsync(favorites, cancellationToken);
        
        return new Response
        {
            Message = "Apartment has been successfully removed from the favorites."
        };
    }
}