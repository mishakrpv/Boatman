using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.UseCases.Dtos;

public class CancelViewingDto
{
    [Required]
    public int ApartmentId { get; set; }
    [Required]
    public int ViewingId { get; set; }
}