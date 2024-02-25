using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.Owner.UseCases.Dtos;

public class UpdateApartmentDto : ApartmentDto
{
    [Required]
    public int ApartmentId { get; set; } = default!;
}