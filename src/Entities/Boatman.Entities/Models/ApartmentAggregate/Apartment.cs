namespace Boatman.Entities.Models.ApartmentAggregate;

public class Apartment : BaseApartment, IAggregateRoot
{
    private readonly List<Request> _requests = [];

    private readonly List<Viewing> _schedule = [];

    public Apartment(string ownerId, decimal rent, string description, int downPaymentInMonths = 1) : base(rent, description, downPaymentInMonths)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; private set; }

    public IEnumerable<Viewing> Schedule => _schedule.AsReadOnly();
    public IEnumerable<Request> Requests => _requests.AsReadOnly();
    public DateTime PublicationDate { get; private set; } = DateTime.Now;

    public bool TryScheduleViewing(string customerId, DateTime start, DateTime end)
    {
        if (start >= end) return false;

        if (Schedule.Any() && start < Schedule.Last().End)
        {
            return false;
        }

        _schedule.Add(new Viewing(customerId, start, end));

        return true;
    }

    public void CancelViewing(int viewingId)
    {
        var viewing = Schedule.FirstOrDefault(v => v.Id == viewingId);

        if (viewing != null)
        {
            _schedule.Remove(viewing);
        }
    }

    public void SubmitAnApplication(string customerId)
    {
        if (Requests.All(r => r.CustomerId != customerId))
        {
            _requests.Add(new Request(customerId));
        }
    }
}