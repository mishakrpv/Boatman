using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.UseCases.Dtos;

public class AddApartmentDto : ApartmentDto
{
    [Required]
    public string OwnerId { get; set; } = default!;
}