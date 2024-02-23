using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.UseCases.Dtos;

public class UpdateApartmentDto : ApartmentDto
{
    [Required]
    public int ApartmentId { get; set; } = default!;
}