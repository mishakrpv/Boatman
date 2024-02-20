namespace Boatman.Entities.Models;

public abstract class BaseEntity<T>
{
    public virtual T Id { get; protected set; } = default!;
}