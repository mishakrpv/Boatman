using Boatman.Entities.Enums;

namespace Boatman.Entities.Models.ApartmentAggregate;

public class Viewing : BaseEntity<int>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Viewing()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        
    }
    
    public Viewing(string customerId, DateTime start, DateTime end)
    {
        CustomerId = customerId;
        Start = start;
        End = end;
    }

    public string CustomerId { get; private set; }

    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public ViewingStatus GetCurrentStatus()
    {
        if (DateTime.Now < Start)
            return ViewingStatus.Pending;
        if (DateTime.Now < End)
            return ViewingStatus.InProcess;
        return ViewingStatus.Completed;
    }
}