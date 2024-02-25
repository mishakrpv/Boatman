using Ardalis.Specification;
using Boatman.Entities.Models;

namespace Boatman.DataAccess.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}