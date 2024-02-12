namespace Boatman.Entities.Models.ApartmentAggregate;

public class Apartment : BaseApartment, IAggregateRoot
{
    public string OwnerId { get; private set; }

    public Apartment(string ownerId, decimal rent, int downPaymentInMonths = 1) : base(rent, downPaymentInMonths)
    {
        OwnerId = ownerId;
    }

    private readonly List<Viewing> _schedule = [];
    private readonly List<Request> _requests = [];

    public IEnumerable<Viewing> Schedule => _schedule.AsReadOnly();
    public IEnumerable<Request> Requests => _requests.AsReadOnly();
    public DateTimeOffset PublicationDate { get; private set; } = DateTimeOffset.Now;

    public bool TryPlanViewing(string customerId, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (!(Schedule.Any() && Schedule.Last().EndTime > startTime))
        {
            _schedule.Add(new Viewing(customerId, startTime, endTime));

            return true;
        }

        return false;
    }
    
    public void DoRequest(string customerId)
    {
        if (Requests.All(r => r.CustomerId != customerId))
        {
            _requests.Add(new Request(customerId));
        }
    }
}