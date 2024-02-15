using System.ComponentModel.DataAnnotations;

namespace Boatman.OwnerApi.UseCases.Dtos;

public class ScheduleViewingDto
{
    [Required] public int ApartmentId { get; set; }

    [Required] public string CustomerId { get; set; } = default!;

    [Required] public DateTime StartTime { get; set; }

    [Required] public DateTime EndTime { get; set; }
}