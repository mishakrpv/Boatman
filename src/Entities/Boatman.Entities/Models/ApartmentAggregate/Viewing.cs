using Boatman.Entities.Enums;

namespace Boatman.Entities.Models.ApartmentAggregate;

public class Viewing : BaseEntity<int>
{
    public Viewing(string customerId, DateTime start, DateTime end)
    {
        CustomerId = customerId;
        StartTime = start;
        EndTime = end;
    }

    public string CustomerId { get; private set; }

    public DateTime StartTime { get; }
    public DateTime EndTime { get; }

    public ViewingStatus GetCurrentStatus()
    {
        if (DateTime.Now < StartTime)
            return ViewingStatus.Pending;
        if (DateTime.Now < EndTime)
            return ViewingStatus.InProcess;
        return ViewingStatus.Completed;
    }
}