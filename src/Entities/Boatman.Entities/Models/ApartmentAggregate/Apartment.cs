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

    public bool TryPlanViewing(string customerId, ViewingDateTime st, ViewingDateTime et)
    {
        if (new DateTimeOffset(st.Year, st.Month, st.Day, st.Hour, st.Minute, 0, TimeSpan.Zero)
            >= new DateTimeOffset(et.Year, et.Month, et.Day, et.Hour, et.Minute, 0, TimeSpan.Zero))
        {
            return false;
        }

        if (Schedule.Any() && Schedule.Last().EndTime >
            new DateTimeOffset(st.Year, st.Month, st.Day, st.Hour, st.Minute, 0, TimeSpan.Zero)) return false;
        _schedule.Add(new Viewing(customerId, st, et));

        return true;
    }
    
    public void DoRequest(string customerId)
    {
        if (Requests.All(r => r.CustomerId != customerId))
        {
            _requests.Add(new Request(customerId));
        }
    }
}