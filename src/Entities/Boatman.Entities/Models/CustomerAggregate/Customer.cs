namespace Boatman.Entities.Models.CustomerAggregate;

public class Customer : BaseUser, IAggregateRoot
{
    public string ExternalId { get; private set; }

    public Customer(string externalId)
    {
        ExternalId = externalId;
    }
}