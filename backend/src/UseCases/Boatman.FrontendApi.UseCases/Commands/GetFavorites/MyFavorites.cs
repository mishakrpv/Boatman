using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.GetFavorites;

public class MyFavorites : IRequest<Response<Favorites>>
{
    public MyFavorites(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
}