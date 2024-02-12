using Boatman.Entities.Enums;

namespace Boatman.Entities.Models.ApartmentAggregate;

public class Viewing : BaseEntity<int>
{
    public string CustomerId { get; private set; }

    public Viewing()
    {
        
    }
    
    public Viewing(string customerId, ViewingDateTime st, ViewingDateTime et)
    {
        CustomerId = customerId;
        StartTime = new DateTimeOffset(st.Year, st.Month, st.Day, st.Hour, st.Minute, 0, TimeSpan.Zero);
        EndTime = new DateTimeOffset(et.Year, et.Month, et.Day, et.Hour, et.Minute, 0, TimeSpan.Zero);
    }

    public Viewing(string customerId, DateTimeOffset st, DateTimeOffset et)
    {
        CustomerId = customerId;
        StartTime = st;
        EndTime = et;
    }

    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }

    public ViewingStatus GetCurrentStatus()
    {
        if (DateTimeOffset.UtcNow < StartTime)
        {
            return ViewingStatus.Pending;
        }
        else if (DateTimeOffset.UtcNow < EndTime)
        {
            return ViewingStatus.InProcess;
        }
        else
        {
            return ViewingStatus.Completed;
        }
    }
}