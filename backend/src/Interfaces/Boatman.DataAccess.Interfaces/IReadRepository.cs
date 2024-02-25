using Ardalis.Specification;
using Boatman.Entities.Models;

namespace Boatman.DataAccess.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}