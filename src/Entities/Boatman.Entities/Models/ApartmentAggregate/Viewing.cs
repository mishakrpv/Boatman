using Boatman.Entities.Enums;

namespace Boatman.Entities.Models.ApartmentAggregate;

public class Viewing : BaseEntity<int>
{
    public string CustomerId { get; private set; }

    public Viewing(string customerId, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        CustomerId = customerId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }

    public ViewingStatus GetCurrentStatus()
    {
        if (DateTimeOffset.Now < StartTime)
        {
            return ViewingStatus.Pending;
        }
        else if (DateTimeOffset.Now < EndTime)
        {
            return ViewingStatus.InProcess;
        }
        else
        {
            return ViewingStatus.Completed;
        }
    }
}