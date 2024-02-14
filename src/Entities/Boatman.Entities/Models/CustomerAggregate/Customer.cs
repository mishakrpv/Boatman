namespace Boatman.Entities.Models.CustomerAggregate;

public class Customer : BaseUser, IAggregateRoot
{
    public Customer(string externalId)
    {
        ExternalId = externalId;
    }

    public string ExternalId { get; private set; }
}