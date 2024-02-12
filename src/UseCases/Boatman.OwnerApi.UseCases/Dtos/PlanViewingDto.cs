namespace Boatman.OwnerApi.UseCases.Dtos;

public class PlanViewingDto
{
    public int ApartmentId { get; set; }
    public string CustomerId { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}