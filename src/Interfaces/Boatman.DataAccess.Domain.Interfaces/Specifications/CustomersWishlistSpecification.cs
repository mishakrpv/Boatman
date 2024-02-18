using Ardalis.Specification;
using Boatman.Entities.Models.WishlistAggregate;

namespace Boatman.DataAccess.Domain.Interfaces.Specifications;

public sealed class CustomersWishlistSpecification  : Specification<Wishlist>
{
    public CustomersWishlistSpecification(string customerId)
    {
        Query
            .Where(w => w.CustomerId == customerId);
    }
}