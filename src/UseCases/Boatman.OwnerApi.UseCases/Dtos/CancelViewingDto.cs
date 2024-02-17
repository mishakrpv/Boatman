using System.ComponentModel.DataAnnotations;

namespace Boatman.OwnerApi.UseCases.Dtos;

public class CancelViewingDto
{
    [Required]
    public int ApartmentId { get; set; }
    [Required]
    public int ViewingId { get; set; }
}