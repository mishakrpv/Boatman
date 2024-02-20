using Ardalis.Specification.EntityFrameworkCore;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models;

namespace Boatman.DataAccess.Domain.Implementations;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DomainContext dbContext) : base(dbContext)
    {
    }
}