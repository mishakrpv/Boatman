using Ardalis.Specification;
using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.DataAccess.Interfaces.Specifications;

public sealed class ApartmentWithPhotosSpecification : Specification<Apartment>
{
    public ApartmentWithPhotosSpecification(int apartmentId)
    {
        Query
            .Where(a => a.Id == apartmentId)
            .Include(a => a.Photos);
    }
}