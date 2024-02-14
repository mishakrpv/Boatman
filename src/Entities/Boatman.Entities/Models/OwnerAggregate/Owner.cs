namespace Boatman.Entities.Models.OwnerAggregate;

public class Owner : BaseUser, IAggregateRoot
{
    public Owner(string externalId)
    {
        ExternalId = externalId;
    }

    public string ExternalId { get; private set; }
}