namespace Boatman.Entities.Models;

public abstract class BaseUser : BaseEntity<int>
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
}