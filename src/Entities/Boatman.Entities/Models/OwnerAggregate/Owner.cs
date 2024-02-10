namespace Boatman.Entities.Models.OwnerAggregate;

public class Owner : BaseUser, IAggregateRoot
{
    public string ExternalId { get; private set; }

    public Owner(string externalId)
    {
        ExternalId = externalId;
    }
}