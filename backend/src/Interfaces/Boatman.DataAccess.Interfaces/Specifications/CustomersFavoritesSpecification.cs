using System.Net.Quic;
using Ardalis.Specification;
using Boatman.Entities.Models.FavoritesAggregate;

namespace Boatman.DataAccess.Interfaces.Specifications;

public sealed class CustomersFavoritesSpecification : Specification<Favorites>
{
    public CustomersFavoritesSpecification(string customerId)
    {
        Query
            .Where(f => f.CustomerId == customerId)
            .Include(f => f.Items);
    }
}