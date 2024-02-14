namespace Boatman.Entities.Models.ApartmentAggregate;

public class Request : BaseEntity<int>
{
    public Request(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
    public DateTime RequestDate { get; private set; } = DateTime.Now;
}