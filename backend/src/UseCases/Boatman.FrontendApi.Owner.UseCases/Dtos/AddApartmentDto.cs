using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.Owner.UseCases.Dtos;

public class AddApartmentDto : ApartmentDto
{
    [Required]
    public string OwnerId { get; set; } = default!;
}