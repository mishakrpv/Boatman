using Ardalis.Specification;
using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.DataAccess.Domain.Interfaces.Specifications;

public sealed class OwnersApartmentSpecification : Specification<Apartment>
{
    public OwnersApartmentSpecification(string ownerId)
    {
        Query
            .Where(a => a.OwnerId == ownerId);
    }
}