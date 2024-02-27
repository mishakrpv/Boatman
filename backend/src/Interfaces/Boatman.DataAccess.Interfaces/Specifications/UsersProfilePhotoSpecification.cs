using Ardalis.Specification;
using Boatman.Entities.Models.ProfilePhotoAggregate;

namespace Boatman.DataAccess.Interfaces.Specifications;

public sealed class UsersProfilePhotoSpecification : Specification<ProfilePhoto>
{
    public UsersProfilePhotoSpecification(string userId)
    {
        Query
            .Where(pp => pp.UserId == userId);
    }
}