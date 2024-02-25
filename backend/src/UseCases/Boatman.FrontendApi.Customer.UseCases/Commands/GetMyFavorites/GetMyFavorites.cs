using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Customer.UseCases.Commands.GetMyFavorites;

public class GetMyFavorites : IRequest<Response<Favorites>>
{
    public GetMyFavorites(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
}