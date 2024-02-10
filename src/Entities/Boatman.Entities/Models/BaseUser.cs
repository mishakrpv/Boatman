namespace Boatman.Entities.Models;

public abstract class BaseUser : BaseEntity<int>
{
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Bio { get; set; } = default!;
}