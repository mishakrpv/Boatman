using Ardalis.Specification.EntityFrameworkCore;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }
}