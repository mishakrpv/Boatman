using Ardalis.Specification;
using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.DataAccess.Domain.Interfaces.Specifications;

public sealed class ApartmentWithScheduleSpecification : Specification<Apartment>
{
    public ApartmentWithScheduleSpecification(int apartmentId)
    {
        Query
            .Where(a => a.Id == apartmentId)
            .Include(a => a.Schedule);
    }
}