namespace Boatman.Entities.Models.ApartmentAggregate;

public class Request : BaseEntity<int>
{
    public Request(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
    public DateTimeOffset RequestDate { get; private set; } = DateTimeOffset.Now;
}